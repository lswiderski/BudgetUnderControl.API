using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Common;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Common.Extensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BudgetUnderControl.Modules.Transactions.Core.ValueObjects;
using AutoMapper;
using BudgetUnderControl.Modules.Transactions.Application.Clients.Files;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.EditTransaction;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.AddTransaction;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Services;
using BudgetUnderControl.Modules.Transactions.Application.Clients.Users;
using BudgetUnderControl.Shared.Abstractions.Contexts;

namespace BudgetUnderControl.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ITagRepository tagRepository;
        private readonly ICurrencyService currencyService;
        private readonly ICurrencyRepository currencyRepository;
        private readonly IMapper mapper;
        private readonly TransactionsContext transactionsContext;
        private readonly IContext context;
        private readonly IFilesApiClient _filesApiClient;

        public TransactionService(TransactionsContext transactionsContext,
            ITransactionRepository transactionRepository,
            ITagRepository tagRepository,
            ICurrencyService currencyService,
            ICurrencyRepository currencyRepository,
            IMapper mapper,
            IContext context, IFilesApiClient filesApiClient)
        {
            this.transactionRepository = transactionRepository;
            this.tagRepository = tagRepository;
            this.currencyService = currencyService;
            this.currencyRepository = currencyRepository;
            this.context = context;
            _filesApiClient = filesApiClient;
            this.transactionsContext = transactionsContext;
            this.mapper = mapper;
        }

        public async Task<ICollection<TransactionListItemDTO>> GetTransactionsAsync(TransactionsFilterDTO filter = null)
        {
            var exchangeRates = (await this.currencyRepository.GetExchangeRatesAsync())
               .Select(x => new ExchangeRateDTO
               {
                   ToCurrencyCode = x.ToCurrency.Code,
                   FromCurrencyCode = x.FromCurrency.Code,
                   Date = x.Date,
                   Rate = x.Rate
               }).ToList();

            var mainCurrency = "PLN";

            var transactions = await this.transactionRepository.GetTransactionsAsync(this.mapper.Map<TransactionsFilter>(filter));
            var dtos = transactions.Select(async t => new TransactionListItemDTO
            {
                AccountId = t.AccountId,
                Date = t.Date,
                Id = t.Id,
                Value = t.Amount,
                Account = t.Account.Name,
                ValueWithCurrency = t.Amount + t.Account.Currency.Symbol,
                ValueInMainCurrency = await this.currencyService.GetValueInCurrencyAsync(exchangeRates, t.Account.Currency.Code, mainCurrency, t.Amount, t.Date),
                Type = t.Type,
                Name = t.Name,
                CurrencyCode = t.Account.Currency.Code,
                IsTransfer = t.IsTransfer,
                ExternalId = t.ExternalId,
                ModifiedOn = t.ModifiedOn,
                CreatedOn = t.CreatedOn,
                CategoryId = t.CategoryId,
                Category = t.Category?.Name,
                Tags = t.TagsToTransaction.Where(x => !x.Tag.IsDeleted).Select(x => new TagDTO { ExternalId = x.Tag.ExternalId, Id = x.Tag.Id, IsDeleted = x.Tag.IsDeleted, Name = x.Tag.Name }).ToList()

            }).Select(t => t.Result)
                .OrderByDescending(t => t.Date)
            .ToList();

            return dtos;
        }

        public async Task AddTransactionAsync(AddTransactionCommand command)
        {
            var user = this.context.Identity;

            if (command.Type == ExtendedTransactionType.Transfer)
            {
                if (command.Amount > 0)
                {
                    command.Amount *= (-1);
                }
                if (command.TransferAmount < 0)
                {
                    command.TransferAmount *= (-1);
                }
                var transactionExpense = Transaction.Create(command.AccountId, TransactionType.Expense, command.Amount, command.Date, command.Name, command.Comment, user.Id, false, command.CategoryId, command.ExternalId, command.Latitude, command.Longitude);
                await transactionRepository.AddTransactionAsync(transactionExpense);

                var transactionIncome = Transaction.Create(command.TransferAccountId.Value, TransactionType.Income, command.TransferAmount, command.TransferDate, command.Name, command.Comment, user.Id, false, command.CategoryId, command.TransferExternalId, command.Latitude, command.Longitude);
                await transactionRepository.AddTransactionAsync(transactionIncome);

                var transfer = Transfer.Create(transactionExpense.Id, transactionIncome.Id, command.Rate);
                await transactionRepository.AddTransferAsync(transfer);
                await this.CreateTagsToTransaction(command.Tags, transactionExpense.Id);
                await this.MergeFiles(command.FileGuid, transactionExpense);
            }
            else
            {
                var type = command.Type.ToTransactionType();
                if (type == TransactionType.Expense && command.Amount > 0)
                {
                    command.Amount *= (-1);
                }
                else if (type == TransactionType.Income && command.Amount < 0)
                {
                    command.Amount *= (-1);
                }
                var transaction = Transaction.Create(command.AccountId, type, command.Amount, command.Date, command.Name, command.Comment, user.Id, false, command.CategoryId, command.ExternalId, command.Latitude, command.Longitude);
                await this.transactionRepository.AddTransactionAsync(transaction);
                await this.CreateTagsToTransaction(command.Tags, transaction.Id);
                await this.MergeFiles(command.FileGuid, transaction);
            }
        }

        public async Task CreateTagsToTransaction(IEnumerable<int> tagsId, int transactionId)
        {
            if (tagsId != null && tagsId.Any())
            {
                var tags2Transactions = tagsId.Select(x => new TagToTransaction
                {
                    TagId = x,
                    TransactionId = transactionId,
                });
                await this.tagRepository.AddAsync(tags2Transactions);
            }
        }

        public async Task EditTransactionAsync(EditTransactionCommand command)
        {
            var user = this.context.Identity;

            var firstTransaction = await this.transactionRepository.GetTransactionAsync(command.Id);
            Transaction secondTransaction = null;

            await transactionRepository.UpdateAsync(firstTransaction);

            //merge tags
            var tags2Transactions = await this.tagRepository.GetTagToTransactionsAsync(command.Id);
            var tags2Add = command.Tags.Except(tags2Transactions.Select(t => t.TagId));
            var tags2Remove = tags2Transactions.Select(t => t.TagId).Except(command.Tags);
            var tags2Transactions2Remove = tags2Transactions.Where(t => tags2Remove.Contains(t.TagId));
            //add new
            await this.CreateTagsToTransaction(tags2Add, firstTransaction.Id);
            //delete removed
            await this.tagRepository.RemoveAsync(tags2Transactions2Remove);

            //merge files
            await this.MergeFiles(command.FileGuid, firstTransaction);

            var transfer = await transactionRepository.GetTransferAsync(command.Id);

            if (transfer != null)
            {
                var secondTransactionId = transfer.ToTransactionId != command.Id ? transfer.ToTransactionId : transfer.FromTransactionId;
                secondTransaction = await this.transactionRepository.GetTransactionAsync(transfer.ToTransactionId);
            }

            //remove transfer, no more transfer
            if (command.ExtendedType != ExtendedTransactionType.Transfer
                && transfer != null && secondTransaction != null)
            {
                await this.transactionRepository.RemoveTransferAsync(transfer);
                await this.transactionRepository.RemoveTransactionAsync(secondTransaction);
                firstTransaction.Edit(command.AccountId, command.ExtendedType.ToTransactionType(), command.Amount, command.Date, command.Name, command.Comment, user.Id, command.IsDeleted, command.CategoryId, command.Latitude, command.Longitude);
                await this.transactionRepository.UpdateAsync(firstTransaction);
            }
            //new Transfer, no transfer before
            else if (command.ExtendedType == ExtendedTransactionType.Transfer
                && transfer == null && secondTransaction == null)
            {
                firstTransaction.Edit(command.AccountId, TransactionType.Expense, command.Amount, command.Date, command.Name, command.Comment, user.Id, command.IsDeleted, command.CategoryId, command.Latitude, command.Longitude);
                await this.transactionRepository.UpdateAsync(firstTransaction);

                var transactionIncome = Transaction.Create(command.TransferAccountId.Value, TransactionType.Income, command.TransferAmount.Value, command.TransferDate.Value, command.Name, command.Comment, user.Id, command.IsDeleted, command.CategoryId, null, command.Latitude, command.Longitude);
                await transactionRepository.AddTransactionAsync(transactionIncome);

                var newTransfer = Transfer.Create(firstTransaction.Id, transactionIncome.Id, command.Rate.Value);
                await transactionRepository.AddTransferAsync(newTransfer);

            }

            //edit transfer
            else if (command.ExtendedType == ExtendedTransactionType.Transfer
                && transfer != null && secondTransaction != null)
            {
                firstTransaction.Edit(command.AccountId, TransactionType.Expense, command.Amount, command.Date, command.Name, command.Comment, user.Id, command.IsDeleted, command.CategoryId, command.Latitude, command.Longitude);
                await this.transactionRepository.UpdateAsync(firstTransaction);

                secondTransaction.Edit(command.TransferAccountId.Value, TransactionType.Income, command.TransferAmount.Value, command.TransferDate.Value, command.Name, command.Comment, user.Id, command.IsDeleted, command.CategoryId, command.Latitude, command.Longitude);
                await this.transactionRepository.UpdateAsync(firstTransaction);

                transfer.SetRate(command.Rate.Value);
                await transactionRepository.UpdateTransferAsync(transfer);
            }
            //just edit 1 transaction, no transfer before
            else if (command.ExtendedType != ExtendedTransactionType.Transfer
                && transfer == null && secondTransaction == null)
            {
                decimal amount = 0;

                if (command.ExtendedType == ExtendedTransactionType.Expense && command.Amount > 0)
                {
                    amount = command.Amount * (-1);
                }
                else if (command.ExtendedType == ExtendedTransactionType.Income && command.Amount < 0)
                {
                    amount = command.Amount * (-1);
                }
                else
                {
                    amount = command.Amount;
                }

                firstTransaction.Edit(command.AccountId, command.ExtendedType.ToTransactionType(), amount, command.Date, command.Name, command.Comment, user.Id, command.IsDeleted, command.CategoryId, command.Latitude, command.Longitude);
                await this.transactionRepository.UpdateAsync(firstTransaction);
            }
        }

        private async Task MergeFiles(string fileGuid, Transaction firstTransaction)
        {
            var fileId = !string.IsNullOrWhiteSpace(fileGuid) ? Guid.Parse(fileGuid) : (Guid?)null;
            var now = DateTime.UtcNow;
            var currentFileId = this.transactionsContext.FilesToTransactions.Where(x => x.TransactionId == firstTransaction.Id && !x.IsDeleted).Select(x => (Guid?)x.FileId).FirstOrDefault();

            if (fileId != null && currentFileId != null)
            {
                if (fileId != currentFileId)  //remove local physical file
                {
                    await _filesApiClient.DeleteFileAsync(currentFileId.Value);
                    var f2Ts = this.transactionsContext.FilesToTransactions.Where(x => x.FileId == currentFileId || x.TransactionId == firstTransaction.Id).ToList();
                    f2Ts.ForEach(x => x.Delete());
                    var guid = Guid.NewGuid();
                    this.transactionsContext.FilesToTransactions.Add(new FileToTransaction
                    {
                        FileId = fileId.Value,
                        TransactionId = firstTransaction.Id,
                        ModifiedOn = now,
                        IsDeleted = false,
                        ExternalId = guid,
                        Id = guid,
                    });
                   
                }
            }
            else if (fileId != null) //assign to transaction
            {
                var guid = Guid.NewGuid();
                
                this.transactionsContext.FilesToTransactions.Add(new FileToTransaction
                {
                    FileId = fileId.Value,
                    TransactionId = firstTransaction.Id,
                    ModifiedOn = now,
                    IsDeleted = false,
                    ExternalId = guid,
                    Id = guid,
                });
            }
            else if (currentFileId != null) //remove old
            {
                await _filesApiClient.DeleteFileAsync(currentFileId.Value);
                var f2Ts = this.transactionsContext.FilesToTransactions.Where(x => x.FileId == currentFileId || x.TransactionId == firstTransaction.Id).ToList();
                f2Ts.ForEach(x => x.Delete());
            }
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(Guid transactionId)
        {
            Transaction firstTransaction = await this.transactionRepository.GetTransactionAsync(transactionId);
            if (firstTransaction == null)
            {
                throw new ArgumentException("No Transaction to remove");
            }

            Transaction secondTransaction = null;
            var transfer = await this.transactionRepository.GetTransferAsync(firstTransaction.Id);
            var tags2Transaction = await this.tagRepository.GetTagToTransactionsAsync(firstTransaction.Id);
            await this.tagRepository.RemoveAsync(tags2Transaction);
            var files = this.transactionsContext.FilesToTransactions.Where(x => x.TransactionId == firstTransaction.Id && !x.IsDeleted).Select(x => x.FileId).ToList();
            foreach (var fileId in files)
            {
                await this.RemoveFileAsync(fileId);
            }

            if (transfer != null)
            {
                var secondTRansactionId = transfer.ToTransactionId != firstTransaction.Id ? transfer.ToTransactionId : transfer.FromTransactionId;
                secondTransaction = await this.transactionRepository.GetTransactionAsync(secondTRansactionId);
                tags2Transaction = await this.tagRepository.GetTagToTransactionsAsync(secondTRansactionId);
                await this.tagRepository.RemoveAsync(tags2Transaction);
                await transactionRepository.RemoveTransferAsync(transfer);
                await transactionRepository.RemoveTransactionAsync(secondTransaction);
            }
            await transactionRepository.RemoveTransactionAsync(firstTransaction);
        }

        public async Task<EditTransactionDTO> GetTransactionAsync(Guid id)
        {
            var transaction = await this.transactionRepository.GetTransactionAsync(id);
            return await this.EntityToEditQueryAsync(transaction);
        }

        public async Task<EditTransactionDTO> GetTransactionAsync(int id)
        {
            var transaction = await this.transactionRepository.GetTransactionAsync(id);
            return await this.EntityToEditQueryAsync(transaction);
        }

        private async Task<EditTransactionDTO> EntityToEditQueryAsync(Transaction entity)
        {
            var transaction = new EditTransactionDTO
            {
                AccountId = entity.AccountId,
                Amount = entity.Amount,
                CategoryId = entity.CategoryId,
                Comment = entity.Comment,
                Date = entity.Date,
                Id = entity.Id,
                Name = entity.Name,
                Type = entity.Type,
                ExternalId = entity.ExternalId,
                ModifiedOn = entity.ModifiedOn,
                CreatedOn = entity.CreatedOn,
                IsDeleted = entity.IsDeleted,
                Longitude = entity.Longitude,
                Latitude = entity.Latitude,
            };

            transaction.ExtendedType = transaction.Type == TransactionType.Income ? ExtendedTransactionType.Income : ExtendedTransactionType.Expense;

            var tags2Transactions = await this.tagRepository.GetTagToTransactionsAsync(transaction.Id);
            var tags = await this.tagRepository.GetAsync(tags2Transactions.Select(t => t.TagId).ToList());
            transaction.Tags = tags.Select(t => new TagDTO
            {
                Id = t.Id,
                Name = t.Name,
                IsDeleted = t.IsDeleted,
                ExternalId = t.ExternalId
            }).ToList();

            var transfer = await this.transactionRepository.GetTransferAsync(transaction.Id);

            if (transfer != null)
            {
                int transferedTransactionId;
                if (transaction.Id == transfer.FromTransactionId)
                {
                    transferedTransactionId = transfer.ToTransactionId;
                }
                else
                {
                    transferedTransactionId = transfer.FromTransactionId;
                }
                var transferedTransaction = await this.transactionRepository.GetTransactionAsync(transferedTransactionId);

                transaction.Rate = transfer.Rate;
                transaction.TransferId = transfer.Id;

                if (transaction.Id == transfer.FromTransactionId)
                {
                    transaction.TransferAmount = transferedTransaction.Amount;
                    transaction.TransferDate = transferedTransaction.Date;
                    transaction.TransferTransactionId = transferedTransaction.Id;
                    transaction.TransferAccountId = transferedTransaction.AccountId;

                }
                else
                {
                    transaction.TransferAmount = transaction.Amount;
                    transaction.Amount = transferedTransaction.Amount;

                    transaction.TransferDate = transaction.Date;
                    transaction.Date = transferedTransaction.Date;

                    transaction.TransferTransactionId = transaction.Id;
                    transaction.Id = transferedTransaction.Id;


                    transaction.TransferAccountId = transaction.AccountId;
                    transaction.AccountId = transferedTransaction.AccountId;
                }

                transaction.ExtendedType = ExtendedTransactionType.Transfer;

            }

            var fileToTransactions = this.transactionsContext.FilesToTransactions.Where(x => x.TransactionId == transaction.Id && !x.IsDeleted).FirstOrDefault();
            if (fileToTransactions != null)
            {
                transaction.FileGuid = fileToTransactions.FileId.ToString();
            }

            return transaction;
        }
        
        public async Task RemoveFileAsync(Guid id)
        {
            //check if user hass access
            //TODO call filesModule to deletee= file
            
            var f2t = this.transactionsContext.FilesToTransactions.Where(x => x.FileId == id).ToList();
            f2t.ForEach(x => x.Delete());
            await this.transactionsContext.SaveChangesAsync();
        }

    }
}

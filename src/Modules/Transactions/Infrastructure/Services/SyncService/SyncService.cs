﻿using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Abstractions.Contexts;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class SyncService : ISyncService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IAccountRepository accountRepository;
        private readonly ICurrencyRepository currencyRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IContext context;
        private readonly ISyncRequestBuilder syncRequestBuilder;
        private readonly ISynchroniser synchroniser;
        private readonly ITagRepository tagRepository;

        private Dictionary<int, int> accountsMap; // key - old AccountId, value - new AccountId
        private Dictionary<int, int> transactionsMap; // key - old TransactionId, value - new TransactionId

        public SyncService(ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,
            ICurrencyRepository currencyRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IContext context,
            ISyncRequestBuilder syncRequestBuilder,
            ISynchroniser synchroniser)
        {
            this.transactionRepository = transactionRepository;
            this.accountRepository = accountRepository;
            this.currencyRepository = currencyRepository;
            this.categoryRepository = categoryRepository;
            this.context = context;

            this.syncRequestBuilder = syncRequestBuilder;
            this.synchroniser = synchroniser;
            this.tagRepository = tagRepository;
        }

        public async Task ImportBackUpAsync(BackUpDTO backupDto)
        {
            await CleanDataBaseAsync();
            //ImportCurrencies(backupDto.Currencies);
            await ImportAccountsAsync(backupDto.Accounts);
            await ImportTransactionsAsync(backupDto.Transactions);
            await ImportTransfersAsync(backupDto.Transfers);
            await ImportTagsAsync(backupDto.Tags);
            await ImportTagsToTransactionsAsync(backupDto.TagsToTransactions);
            await ImportExchangeRatesAsync(backupDto.ExchangeRates);
        }

        public async Task<BackUpDTO> GetBackUpAsync()
        {
            var currencies = (await this.currencyRepository.GetCurriencesAsync()).Select(x => new CurrencySyncDTO
            {
                Code = x.Code,
                FullName = x.FullName,
                Id = x.Id,
                Number = x.Number,
                Symbol = x.Symbol
            }).ToList();

            var accounts = (await this.accountRepository.GetAccountsAsync()).Select(x => new AccountSyncDTO
            {
                Id = x.Id,
                ExternalId = x.ExternalId,
                Comment = x.Comment,
                CurrencyId = x.CurrencyId,
                IsIncludedToTotal = x.IsIncludedToTotal,
                Name = x.Name,
                Order = x.Order,
                ParentAccountId = x.ParentAccountId,
                Type = x.Type,
            }).ToList();

            var transfers = (await this.transactionRepository.GetTransfersAsync()).Select(x => new TransferSyncDTO
            {
                Id = x.Id,
                FromTransactionId = x.FromTransactionId,
                Rate = x.Rate,
                ToTransactionId = x.ToTransactionId,
                ExternalId = x.ExternalId,
            }).ToList();

            var transactions = (await this.transactionRepository.GetTransactionsAsync()).Select(x =>
                new TransactionSyncDTO
                {
                    Name = x.Name,
                    Comment = x.Comment,
                    AccountId = x.AccountId,
                    Amount = x.Amount,
                    CategoryId = x.CategoryId,
                    CreatedOn = x.CreatedOn,
                    Date = x.Date,
                    Id = x.Id,
                    ExternalId = x.ExternalId,
                    ModifiedOn = x.ModifiedOn,
                    Type = x.Type,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    IsDeleted = x.IsDeleted,
                }).ToList();

            var tags = (await this.tagRepository.GetAsync())
                .Select(x => new TagSyncDTO
                {
                    Id = x.Id,
                    ExternalId = x.ExternalId,
                    Name = x.Name,
                    ModifiedOn = x.ModifiedOn,
                    IsDeleted = x.IsDeleted
                }).ToList();

            var t2t = (await this.tagRepository.GetTagToTransactionsAsync())
                .Select(x => new TagToTransactionSyncDTO
                {
                    Id = x.Id,
                    TagId = x.Tag.ExternalId,
                    TransactionId = x.Transaction.ExternalId,
                }).ToList();

            var exchangeRates = (await this.currencyRepository.GetExchangeRatesAsync())
                .Select(x => new ExchangeRateSyncDTO
                {
                    Rate = x.Rate,
                    Date = x.Date,
                    FromCurrency = x.FromCurrency.Code,
                    ToCurrency = x.ToCurrency.Code,
                    ExternalId = x.ExternalId,
                    IsDeleted = x.IsDeleted,
                    ModifiedOn = x.ModifiedOn
                }).ToList();

            var backUp = new BackUpDTO
            {
                Currencies = currencies,
                Accounts = accounts,
                Transfers = transfers,
                Transactions = transactions,
                Tags = tags,
                TagsToTransactions = t2t,
                ExchangeRates = exchangeRates,
            };

            return backUp;
        }


        public async Task CleanDataBaseAsync()
        {
            var transfers = await this.transactionRepository.GetTransfersAsync();
            await this.transactionRepository.HardRemoveTransfersAsync(transfers);

            var transactions = await this.transactionRepository.GetTransactionsAsync();
            await this.transactionRepository.HardRemoveTransactionsAsync(transactions);

            var accounts = await this.accountRepository.GetAccountsAsync();
            await this.accountRepository.HardRemoveAccountsAsync(accounts);

            var tags = await this.tagRepository.GetAsync();
            await this.tagRepository.RemoveAsync(tags);

            //this.Context.Currencies.RemoveRange(this.Context.Currencies);
        }

        private async Task ImportAccountsAsync(List<AccountSyncDTO> accounts)
        {
            accountsMap = new Dictionary<int, int>();

            foreach (var item in accounts)
            {
                var account = Account.Create(item.Name, item.CurrencyId, item.IsIncludedToTotal,
                    item.Comment, item.Order, item.Type, item.ParentAccountId, item.IsActive, item.IsDeleted, context.Identity.Id,
                    item.ExternalId);
                await this.accountRepository.AddAccountAsync(account);

                accountsMap.Add(item.Id, account.Id);
            }

            //need to fix parentsAccountsIds
            await this.FixParentsAccountsIdsAsync();
        }

        private async Task FixParentsAccountsIdsAsync()
        {
            var accounts = (await this.accountRepository.GetAccountsAsync()).ToArray();

            for (int i = 0; i < accounts.Count(); i++)
            {
                if (accounts[i].ParentAccountId != null && accountsMap.ContainsKey(accounts[i].ParentAccountId.Value))
                {
                    accounts[i].SetParentAccountId(accountsMap[accounts[i].ParentAccountId.Value]);

                    await this.accountRepository.UpdateAsync(accounts[i]);
                }
            }
        }

        private async Task ImportTransactionsAsync(List<TransactionSyncDTO> transactions)
        {
            var tempTransactionsMap = new Dictionary<int, Domain.Transaction>();
            var categories = await categoryRepository.GetCategoriesAsync();
            transactionsMap = new Dictionary<int, int>();
            foreach (var item in transactions)
            {
                int? categoryId = null;
                if (categories.Any(x => x.Id == item.CategoryId))
                {
                    categoryId = item.CategoryId;
                }

                var transaction = Domain.Transaction.Create(accountsMap[item.AccountId], item.Type, item.Amount,
                    item.Date, item.Name, item.Comment, context.Identity.Id, item.IsDeleted, categoryId,
                    item.ExternalId, item.Latitude, item.Longitude);
                transaction.SetCreatedOn(item.CreatedOn);
                transaction.SetModifiedOn(item.ModifiedOn);
                tempTransactionsMap.Add(item.Id, transaction);
                //await this.transactionRepository.AddTransactionAsync(transaction);
                //transactionsMap.Add(item.Id, transaction.Id);
            }

            await this.transactionRepository.AddTransactionsAsync(tempTransactionsMap.Select(x => x.Value));
            transactionsMap = tempTransactionsMap.ToDictionary(x => x.Key, x => x.Value.Id);
        }

        private async Task ImportTransfersAsync(List<TransferSyncDTO> transfers)
        {
            foreach (var item in transfers)
            {
                if (transactionsMap.ContainsKey(item.FromTransactionId) &&
                    transactionsMap.ContainsKey(item.ToTransactionId))
                {
                    var transfer = Transfer.Create(transactionsMap[item.FromTransactionId],
                        transactionsMap[item.ToTransactionId], item.Rate, item.ExternalId);
                    await this.transactionRepository.AddTransferAsync(transfer);
                }
            }
        }

        private async Task ImportTagsAsync(List<TagSyncDTO> tags)
        {
            foreach (var item in tags)
            {
                var tag = Tag.Create(item.Name, context.Identity.Id, item.IsDeleted, item.ExternalId);
                tag.SetModifiedOn(item.ModifiedOn);
                await this.tagRepository.AddAsync(tag);
            }
        }

        private async Task ImportTagsToTransactionsAsync(List<TagToTransactionSyncDTO> t2ts)
        {
            var tagIds = t2ts.Select(x => x.TagId).ToList();
            var transactionIds = t2ts.Select(x => x.TransactionId).ToList();
            var tags = (await this.tagRepository.GetAsync(tagIds))
                .ToDictionary(x => x.ExternalId, x => x);
            var transactions = (await this.transactionRepository.GetTransactionsAsync())
                .Where(x => transactionIds.Contains(x.ExternalId)).ToList()
                .ToDictionary(x => x.ExternalId, x => x);

            foreach (var item in t2ts)
            {
                var t2t = new TagToTransaction
                {
                    Tag = tags[item.TagId],
                    Transaction = transactions[item.TransactionId],
                };

                await this.tagRepository.AddAsync(t2t);
            }
        }

        private async Task ImportExchangeRatesAsync(List<ExchangeRateSyncDTO> exchangeRates)
        {
            if (exchangeRates != null && exchangeRates.Any())
            {
                var currenciesDict = (await this.currencyRepository.GetCurriencesAsync())
                    .ToDictionary(x => x.Code, x => x.Id);
                foreach (var item in exchangeRates)
                {
                    var fromCurrencyId = currenciesDict.ContainsKey(item.FromCurrency)
                        ? currenciesDict[item.FromCurrency]
                        : (int?)null;
                    var toCurrencyId = currenciesDict.ContainsKey(item.ToCurrency)
                        ? currenciesDict[item.ToCurrency]
                        : (int?)null;
                    if (fromCurrencyId.HasValue && toCurrencyId.HasValue)
                    {
                        var exchangeRate = ExchangeRate.Create(fromCurrencyId.Value, toCurrencyId.Value, item.Rate,
                            context.Identity.Id, item.ExternalId, item.IsDeleted, item.Date);
                        await this.currencyRepository.AddExchangeRateAsync(exchangeRate);
                    }
                }
            }
        }

        private async Task ImportCurrenciesAsync(List<CurrencySyncDTO> currencies)
        {
            foreach (var item in currencies)
            {
                var currency = Currency.Create(item.Code, item.FullName, item.Number, item.Symbol);
                currency.SetId(item.Id);
                await this.currencyRepository.AddCurrencyAsync(currency);
            }
        }

        public async Task<SyncRequest> SyncAsync(SyncRequest request)
        {
            //get requst
            var newRequest =
                await this.syncRequestBuilder.CreateSyncRequestAsync(SynchronizationComponent.Api,
                    request.Component); // source = this. TODO on future. For now allow sync only on route mobile -> api -> mobile
            //update own collection
            await this.synchroniser.SynchroniseAsync(request);
            //send responserequest

            return newRequest;
        }
    }
}
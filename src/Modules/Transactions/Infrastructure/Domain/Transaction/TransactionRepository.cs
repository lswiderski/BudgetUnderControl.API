using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Modules.Transactions.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Shared.Abstractions.Contexts;

namespace BudgetUnderControl.Infrastructure
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IAccountRepository accountRepository;
        private readonly IContext context;
        private readonly TransactionsContext transactionsContext;

        public TransactionRepository(TransactionsContext transactionsContext, IAccountRepository accountRepository,
            IContext context)
        {
            this.accountRepository = accountRepository;
            this.context = context;
            this.transactionsContext = transactionsContext;
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            this.transactionsContext.Transactions.Add(transaction);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task AddTransactionsAsync(IEnumerable<Transaction> transactions)
        {
            this.transactionsContext.Transactions.AddRange(transactions);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            transaction.UpdateModify();
            this.transactionsContext.Transactions.Update(transaction);
            //await this.Context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(IEnumerable<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                transaction.UpdateModify();
            }

            this.transactionsContext.Transactions.UpdateRange(transactions);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task AddTransferAsync(Transfer transfer)
        {
            this.transactionsContext.Transfers.Add(transfer);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task UpdateTransferAsync(Transfer transfer)
        {
            transfer.UpdateModify();
            this.transactionsContext.Transfers.Update(transfer);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task RemoveTransactionAsync(Transaction transaction)
        {
            transaction.Delete();
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task RemoveTransferAsync(Transfer transfer)
        {
            transfer.Delete();
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task HardRemoveTransactionsAsync(IEnumerable<Transaction> transactions)
        {
            this.transactionsContext.Transactions.RemoveRange(transactions);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task HardRemoveTransfersAsync(IEnumerable<Transfer> transfers)
        {
            this.transactionsContext.Transfers.RemoveRange(transfers);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task<ICollection<Transaction>> GetTransactionsAsync(TransactionsFilter filter = null)
        {

            var query = this.transactionsContext.Transactions
                        .Include(p => p.Category)
                        .Include(p => p.Account)
                            .ThenInclude(p => p.Currency)
                        .Include(p => p.TagsToTransaction)
                            .ThenInclude(p => p.Tag)
                        .Include(p => p.ToTransfers)
                        .Include(p => p.FromTransfers)
                        .Include(p => p.FilesToTransaction)
                            .ThenInclude(p => p.File)
                        .AsQueryable();


            if (filter != null && filter.AccountsIds != null && filter.AccountsIds.Any())
            {
                var accounts = await this.accountRepository.GetSubAccountsAsync(filter.AccountsIds);
                accounts.AddRange(filter.AccountsIds);
                accounts = accounts.Distinct().ToList();

                query = query.Where(q => accounts.Contains(q.AccountId)).AsQueryable();
            }
            else if (filter != null && filter.AccountsExternalIds != null && filter.AccountsExternalIds.Any())
            {
                var accounts = await this.accountRepository.GetSubAccountsAsync(filter.AccountsExternalIds);
                accounts.AddRange(filter.AccountsExternalIds);
                accounts = accounts.Distinct().ToList();

                query = query.Where(q => accounts.Contains(q.Account.ExternalId)).AsQueryable();
            }
            else
            {
                query = query.Where(q => q.Account.UserId == context.Identity.Id).AsQueryable();
            }

            if (filter != null && filter.CategoryIds != null && filter.CategoryIds.Any())
            {
                query = query.Where(q => filter.CategoryIds.Contains(q.CategoryId)).AsQueryable();
            }

            if (filter != null && filter.TagIds != null && filter.TagIds.Any())
            {
                query = (from transaction in query
                         join t2t in this.transactionsContext.TagsToTransactions on transaction.Id equals t2t.TransactionId
                         where filter.TagIds.Contains(t2t.TagId)
                         select transaction).AsQueryable();
            }


            if (filter != null)
            {
                if (!filter.IncludeDeleted)
                {
                    query = query.Where(q => q.IsDeleted == false).AsQueryable();
                }


                if (filter.FromDate != null)
                {
                    query = query.Where(q => q.Date >= filter.FromDate).AsQueryable();
                }

                if (filter.ToDate != null)
                {
                    query = query.Where(q => q.Date <= filter.ToDate).AsQueryable();
                }

                if (filter.ChangedSince != null)
                {
                    query = query.Where(q => q.CreatedOn >= filter.ChangedSince || q.ModifiedOn >= filter.ChangedSince).AsQueryable();
                }



            }
            else
            {
                query = query.Where(q => q.IsDeleted == false).AsQueryable();
            }

            var temporary = await query.ToListAsync();
            var transactionsWithExtraProperty = (await (from t in query
                                                        orderby t.Date descending
                                                        select new
                                                        {
                                                            t,
                                                            IsTransfer = t.ToTransfers.Any() || t.FromTransfers.Any(),
                                                        })
                                                   .ToListAsync());
            transactionsWithExtraProperty.ForEach(x => x.t.IsTransfer = x.IsTransfer);

            if (filter != null && !filter.IncludeTransfers)
            {
                transactionsWithExtraProperty = transactionsWithExtraProperty.Where(q => q.IsTransfer == false).ToList();
            }

            var transactions = transactionsWithExtraProperty.Select(x => x.t).ToList();
            return transactions;



        }

        public async Task<Transaction> GetTransactionAsync(int id)
        {
            var transaction = await this.transactionsContext.Transactions.Where(t => t.Id == id).SingleOrDefaultAsync();
            return transaction;
        }

        public async Task<Transaction> GetTransactionAsync(Guid id)
        {
            var transaction = await this.transactionsContext.Transactions.Where(t => t.ExternalId.ToString() == id.ToString()).SingleOrDefaultAsync();
            return transaction;
        }

        public async Task<Transfer> GetTransferAsync(int transactionId)
        {
            var transfer = await this.transactionsContext.Transfers.Where(t => t.FromTransactionId == transactionId || t.ToTransactionId == transactionId).SingleOrDefaultAsync();
            return transfer;
        }

        public async Task<Transfer> GetTransferAsync(Guid transactionId)
        {
            var transfer = await this.transactionsContext.Transfers.Where(t => t.ExternalId == transactionId).SingleOrDefaultAsync();
            return transfer;
        }


        public async Task<ICollection<Transfer>> GetTransfersAsync()
        {
            var transfers = await this.transactionsContext.Transfers
                .Include(t => t.FromTransaction)
                .Include(t => t.ToTransaction)
                .ToListAsync();

            return transfers;
        }

        public async Task<ICollection<Transfer>> GetTransfersModifiedSinceAsync(DateTime changedSince)
        {
            var transfers = await this.transactionsContext.Transfers
                .Include(t => t.FromTransaction)
                .Include(t => t.ToTransaction)
                .Where(q => q.ModifiedOn >= changedSince)
                .ToListAsync();

            return transfers;
        }

    }
}

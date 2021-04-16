using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Infrastructure.Services;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Clients.Users;
using BudgetUnderControl.Shared.Abstractions.Contexts;

namespace BudgetUnderControl.Infrastructure
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TransactionsContext transactionsContext;
        private readonly IContext context;

        public AccountRepository(TransactionsContext transactionsContext, IContext context)
        {
            this.transactionsContext = transactionsContext;
            this.context = context;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(bool? active = null)
        {
            var query = this.transactionsContext.Accounts.AsQueryable();

            if (active.HasValue)
            {
                query = query.Where(a => a.IsActive == active).AsQueryable();

            }

            var accounts = await (from account in query
                                  join currency in this.transactionsContext.Currencies on account.CurrencyId equals currency.Id
                                  where account.UserId == context.Identity.Id
                                  select account)
                                 .Include(p => p.Currency)
                                 .OrderBy(a => a.Order)
                                 .ToListAsync();
            return accounts;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync(bool? active = null)
        {
            var query = this.transactionsContext.Accounts.AsQueryable();

            if (active.HasValue)
            {
                query = query.Where(a => a.IsActive == active).AsQueryable();
            }
            var accounts = await (from account in query
                                  join currency in this.transactionsContext.Currencies on account.CurrencyId equals currency.Id
                                  select account)
                                 .Include(p => p.Currency)
                                 .OrderBy(a => a.Order)
                                 .ToListAsync();
            return accounts;
        }

        public async Task AddAccountAsync(Account account)
        {
            this.transactionsContext.Accounts.Add(account);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task HardRemoveAccountsAsync(IEnumerable<Account> accounts)
        {
            this.transactionsContext.Accounts.RemoveRange(accounts);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task<Account> GetAccountAsync(int id)
        {
            var acc = await (from account in this.transactionsContext.Accounts
                             join currency in this.transactionsContext.Currencies on account.CurrencyId equals currency.Id
                             where account.Id == id
                             select account
                       ).Include(p => p.Currency)
                       .FirstOrDefaultAsync();

            return acc;
        }

        public async Task<Account> GetAccountAsync(Guid id)
        {
            var acc = await (from account in this.transactionsContext.Accounts
                             join currency in this.transactionsContext.Currencies on account.CurrencyId equals currency.Id
                             where account.ExternalId == id
                             select account
                       ).Include(p => p.Currency)
                       .FirstOrDefaultAsync();

            return acc;
        }

        public async Task UpdateAsync(Account account)
        {
            account.UpdateModify();
            this.transactionsContext.Accounts.Update(account);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task<decimal> GetActualBalanceAsync(int accountId)
        {
            var isCard = await IsSubCardAccountAsync(accountId);

            if (isCard)
            {
                accountId = (await this.GetParentAccountIdAsync(accountId)).Value;
            }

            var accounts = await this.GetSubAccountsAsync(new List<int> { accountId }, true);
            accounts.Add(accountId);
            accounts = accounts.Distinct().ToList();
            var transactions = await this.transactionsContext.Transactions.Where(x => accounts.Contains(x.AccountId) && !x.IsDeleted).Select(x => (decimal)x.Amount).ToListAsync();
            var balance = transactions.Sum(x => (decimal)x);

            return balance;
        }

        public async Task<decimal> GetIncomeAsync(int accountId, DateTime? fromDate, DateTime? toDate)
        {
            var isCard = await IsSubCardAccountAsync(accountId);

            if (isCard)
            {
                accountId = (await this.GetParentAccountIdAsync(accountId)).Value;
            }

            var accounts = await this.GetSubAccountsAsync(new List<int> { accountId }, true);
            accounts.Add(accountId);
            accounts = accounts.Distinct().ToList();

            var balance = this.transactionsContext.Transactions.Where(x => accounts.Contains(x.AccountId) && x.Amount > 0 && x.Date >= fromDate && x.Date <= toDate && !x.IsDeleted).Select(x => (decimal)x.Amount).ToList().Sum(x => (decimal)x);
            return balance;
        }

        public async Task<decimal> GetExpenseAsync(int accountId, DateTime? fromDate, DateTime? toDate)
        {
            var isCard = await IsSubCardAccountAsync(accountId);

            if (isCard)
            {
                accountId = (await this.GetParentAccountIdAsync(accountId)).Value;
            }

            var accounts = await this.GetSubAccountsAsync(new List<int> { accountId }, true);
            accounts.Add(accountId);
            accounts = accounts.Distinct().ToList();

            var balance = this.transactionsContext.Transactions.Where(x => accounts.Contains(x.AccountId) && x.Amount < 0 && x.Date >= fromDate && x.Date <= toDate && !x.IsDeleted).Select(x => (decimal)x.Amount).ToList().Sum(x => (decimal)x);
            return balance;
        }

        public async Task BalanceAdjustmentAsync(int accountId, decimal targetBalance)
        {
            decimal actualBalance = await GetActualBalanceAsync(accountId);

            if (!decimal.Equals(actualBalance, targetBalance))
            {
                decimal amount = (decimal.Subtract(targetBalance, actualBalance));
                var type = Math.Sign(amount) < 0 ? TransactionType.Expense : TransactionType.Income;
                var defaultCategoryId = this.transactionsContext.Categories.Where(x => x.IsDefault && !x.IsDeleted).Select(x => (int?)x.Id).FirstOrDefault();
                var transaction = Transaction.Create(accountId, type, amount, DateTime.UtcNow, "BalanceAdjustment", string.Empty, this.context.Identity.Id, false, defaultCategoryId);

                this.transactionsContext.Transactions.Add(transaction);
                await this.transactionsContext.SaveChangesAsync();
            }
        }

        public async Task<List<int>> GetSubAccountsAsync(IEnumerable<int> accountsIds, bool? active = null)
        {
            var query = this.transactionsContext.Accounts.AsQueryable();

            if (active.HasValue)
            {
                query = query.Where(a => a.IsActive == active).AsQueryable();
            }

            var subAccounts = await query
                .Where(x => x.ParentAccountId.HasValue 
                && accountsIds.Contains(x.ParentAccountId.Value)
                && x.IsActive == true)
                .Select(x => x.Id)
                .ToListAsync();
            return subAccounts;
        }

        public async Task<List<Guid>> GetSubAccountsAsync(IEnumerable<Guid> accountsExternalIds, bool? active = null)
        {
            var accountsIds = await this.transactionsContext.Accounts.Where(x => accountsExternalIds.Contains(x.ExternalId)).Select(x => x.Id).ToListAsync();
            var query = this.transactionsContext.Accounts.AsQueryable();
            if (active.HasValue)
            {
                query = query.Where(a => a.IsActive == active).AsQueryable();
            }

            var subAccounts = await query
                .Where(x => x.ParentAccountId.HasValue
                && accountsIds.Contains(x.ParentAccountId.Value)
                && x.IsActive == true)
                .Select(x => x.ExternalId)
                .ToListAsync();
            return subAccounts;
        }

        private async Task<bool> IsSubCardAccountAsync(int accountId)
        {
            var result = await this.transactionsContext.Accounts.AnyAsync(x => x.Id == accountId && x.ParentAccountId.HasValue && x.Type == AccountType.Card);
            return result;
        }

        private async Task<int?> GetParentAccountIdAsync(int accountId)
        {
            var result = await this.transactionsContext.Accounts.Where(x => x.Id == accountId).Select(x => x.ParentAccountId).FirstOrDefaultAsync();
            return result;
        }
    }
}

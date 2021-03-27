using BudgetUnderControl.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain.Repositiories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccountsAsync(bool? active = null);
        Task<IEnumerable<Account>> GetAllAccountsAsync(bool? active = null);
        Task<Account> GetAccountAsync(int id);
        Task<Account> GetAccountAsync(Guid id);
        Task UpdateAsync(Account account);
        Task<decimal> GetActualBalanceAsync(int accountId);
        Task AddAccountAsync(Account account);
        Task BalanceAdjustmentAsync(int accountId, decimal targetBalance);
        Task<List<int>> GetSubAccountsAsync(IEnumerable<int> accountsIds, bool? active = null);
        Task<List<Guid>> GetSubAccountsAsync(IEnumerable<Guid> accountsExternalIds, bool? active = null);
        Task<decimal> GetExpenseAsync(int accountId, DateTime? fromDate, DateTime? toDate);
        Task<decimal> GetIncomeAsync(int accountId, DateTime? fromDate, DateTime? toDate);
        Task HardRemoveAccountsAsync(IEnumerable<Account> accounts);




    }
}

using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface IAccountService
    {
        Task ActivateAccountAsync(int id);
        Task DeactivateAccountAsync(int id);

        Task<ICollection<AccountListItemDTO>> GetAccountsWithBalanceAsync();
        Task<EditAccountDTO> GetAccountAsync(Guid id);
        Task<AccountDetailsDTO> GetAccountDetailsAsync(TransactionsFilter filter);

        Task AddAccountAsync(AddAccount account);
        Task EditAccountAsync(EditAccount command);
        Task DeleteAccountAsync(DeleteAccount command);
        Task<bool> IsValidAsync(int accountId);
    }
}

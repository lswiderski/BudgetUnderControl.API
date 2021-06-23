using BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.CreateAccount;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.UpdateAccount;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface IAccountService
    {
        Task ActivateAccountAsync(int id);
        Task DeactivateAccountAsync(int id);

        Task<ICollection<AccountListItemDTO>> GetAccountsWithBalanceAsync(bool onlyActive);
        Task<EditAccountDTO> GetAccountAsync(Guid id);
        Task<AccountDetailsDTO> GetAccountDetailsAsync(TransactionsFilterDTO filter);

        Task AddAccountAsync(CreateAccountCommand account);
        Task EditAccountAsync(UpdateAccountCommand command);
        Task DeleteAccountAsync(Guid id);
        Task<bool> IsValidAsync(int accountId);
    }
}

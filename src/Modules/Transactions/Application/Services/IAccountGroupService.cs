using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface IAccountGroupService
    {
        Task<ICollection<AccountGroupItemDTO>> GetAccountGroupsAsync();
        Task<AccountGroupItemDTO> GetAccountGroupAsync(Guid id);
        Task<bool> IsValidAsync(int id);
    }
}

using BudgetUnderControl.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface IAccountGroupService
    {
        Task<ICollection<AccountGroupItemDTO>> GetAccountGroupsAsync();
        Task<AccountGroupItemDTO> GetAccountGroupAsync(Guid id);
        Task<bool> IsValidAsync(int id);
    }
}

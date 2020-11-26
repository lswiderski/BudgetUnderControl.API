using BudgetUnderControl.Common.Contracts.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Interfaces
{
    public interface IUserAdminService
    {
        Task<ICollection<UserListItemDTO>> GetUsersAsync();
        Task<UserDTO> GetUserAsync(Guid id);
    }
}

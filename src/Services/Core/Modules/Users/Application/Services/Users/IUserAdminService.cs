
using BudgetUnderControl.Modules.Users.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Application.Services
{
    public interface IUserAdminService
    {
        Task<ICollection<UserListItemDTO>> GetUsersAsync();
        Task<UserDTO> GetUserAsync(Guid id);
    }
}

using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface IUserAdminService
    {
        Task<ICollection<UserListItemDTO>> GetUsersAsync();
        Task<UserDTO> GetUserAsync(Guid id);
    }
}

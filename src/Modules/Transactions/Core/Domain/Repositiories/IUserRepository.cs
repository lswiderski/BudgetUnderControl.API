using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain.Repositiories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetAsync(string username);
        Task<User> GetAsync(Guid id);
        Task<ICollection<User>> GetUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}

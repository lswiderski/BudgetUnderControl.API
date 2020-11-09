using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain.Repositiories
{
    public interface IUserRepository
    {
        Task<User> GetFirstUserAsync();
        Task<User> GetByEmailAsync(string email);
        Task<User> GetAsync(string username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}

using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Infrastructure.Repositories
{
    public class UserRepository : BaseModel, IUserRepository
    {
        public UserRepository(IContextFacade context) : base(context)
        {
        }

        public async Task<User> GetAsync(string username)
        {
            var user = await this.Context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));

            return user;
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await this.Context.Users.FirstOrDefaultAsync(u => u.ExternalId.Equals(id));

            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await this.Context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

            return user;
        }

        public async Task<ICollection<User>> GetUsersAsync()
        {
            var users = await this.Context.Users.ToListAsync();

            return users;
        }

        public async Task UpdateUserAsync(User user)
        {
            user.UpdateModify();
            this.Context.Users.Update(user);
            await this.Context.SaveChangesAsync();
        }

        public async Task AddUserAsync(User user)
        {
            user.UpdateModify();
            await this.Context.Users.AddAsync(user);
            await this.Context.SaveChangesAsync();
        }
    }
}

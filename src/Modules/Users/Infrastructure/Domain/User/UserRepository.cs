﻿using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Users.Domain.Repositories;
using BudgetUnderControl.Modules.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Users.Infrastructure.DataAccess;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Domain
{
    public class UserRepository : IUserRepository
    {
        private readonly UsersDbContext Context;

        public UserRepository(UsersDbContext context)
        {
            this.Context = context;
        }

        public async Task<User> GetAsync(string username)
        {
            var user = await this.Context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));

            return user;
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await this.Context.Users.FirstOrDefaultAsync(u => u.Id.Equals(id));

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

using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Collections.Generic;
using BudgetUnderControl.Common;
using System;
using BudgetUnderControl.Common.Enums;

using System.Data.Common;
using BudgetUnderControl.Modules.Users.Domain.Entities;

namespace BudgetUnderControl.Domain
{
    public class UsersDbContext : DbContext, IDisposable
    {
        public virtual DbSet<User> Users { get; set; }


        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            modelBuilder.Entity<User>().ToTable("User");
        }

    }
}

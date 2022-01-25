using System;
using BudgetUnderControl.Modules.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BudgetUnderControl.Modules.Users.Infrastructure.DataAccess
{
    public class UsersDbContext : DbContext, IDisposable
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }


        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Token>().ToTable("Token");
            
            
            modelBuilder.Entity<Token>()
                .HasOne(x => x.User)
                .WithMany(y => y.Tokens)
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("ForeignKey_Token_User");
        }

    }
}

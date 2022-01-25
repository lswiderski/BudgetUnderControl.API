using System;
using BudgetUnderControl.Modules.Files.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetUnderControl.Modules.Files.Core.DataAccess
{
    public class FilesDbContext: DbContext, IDisposable
    {
        public virtual DbSet<File> Files { get; set; }


        public FilesDbContext(DbContextOptions<FilesDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("files");
            modelBuilder.Entity<File>().ToTable("File");

        }

    }
}

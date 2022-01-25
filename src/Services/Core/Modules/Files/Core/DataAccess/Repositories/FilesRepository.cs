using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Files.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetUnderControl.Modules.Files.Core.DataAccess.Repositories
{
    internal sealed class FilesRepository : IFilesRepository
    {
        private readonly FilesDbContext _dbContext;

        public FilesRepository(FilesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<File> GetAsync(Guid id)
            => await _dbContext.Files.SingleOrDefaultAsync(s => s.Id == id);

        public async Task<List<File>> GetAsync(Guid userId, DateTime changedSince)
        {
            return await _dbContext.Files
                .Where(x => x.UserId == userId && x.ModifiedOn >= changedSince)
                .ToListAsync();
        }


        public async Task<bool> ExistsAsync(Guid id)
            => await  _dbContext.Files.AnyAsync(s => s.Id == id);
        
        public async Task AddAsync(File file)
        {
            await _dbContext.Files.AddAsync(file);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(File file)
        {
            _dbContext.Files.Update(file);
            await _dbContext.SaveChangesAsync();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Files.Core.Entities;

namespace BudgetUnderControl.Modules.Files.Core.DataAccess.Repositories
{
    public interface IFilesRepository
    {
        Task<File> GetAsync(Guid id);
        Task<List<File>> GetAsync(Guid userId, DateTime changedSince);
        Task<bool> ExistsAsync(Guid id);
        Task AddAsync(File file);
        Task UpdateAsync(File file);
    }
}
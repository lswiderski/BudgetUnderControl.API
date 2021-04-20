using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Clients.Files.DTO;

namespace BudgetUnderControl.Modules.Transactions.Application.Clients.Files
{
    public interface IFilesApiClient
    {
        Task<List<FileDto>> GetFilesAsync(Guid userId, DateTime changedSince);
        
        Task<byte[]> GetFileContentAsync(Guid id);
        
        Task DeleteFileAsync(Guid id);

        Task CreateFileAsync(CreateFileRequest request);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Files.Core.DTO;
using Microsoft.AspNetCore.Http;

namespace BudgetUnderControl.Modules.Files.Core.Services
{
    public interface IFileService
    {
        Task<Guid> SaveFileAsync(IFormFile file);

        Task<string> SaveFileContentAsync(byte[] content, Guid id, DateTime? date = null);

        Task<FileDto> GetFileAsync(Guid id, string token);

        Task<List<FileDto>> GetFilesAsync(Guid userId, DateTime changedSince);

        Task<byte[]> GetFileBytesAsync(Guid id);

        Task RemoveFileAsync(Guid id);

        void RemoveFileContent(Guid id, Guid userId, DateTime createdOn);
    }
}

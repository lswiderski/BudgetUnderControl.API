using BudgetUnderControl.Common.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface IFileService
    {
        Task<Guid> SaveFileAsync(IFormFile file);

        Task<string> SaveFileAsync(byte[] content, Guid id, DateTime? date = null);

        Task<FileDto> GetFileAsync(Guid id, string token);

        Task<byte[]> GetFileBytesAsync(Guid id);

        Task RemoveFileAsync(Guid id);

        void RemoveFileContent(Guid id, Guid userId, DateTime createdOn);
    }
}

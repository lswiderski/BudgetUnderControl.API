using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Clients.Files;
using BudgetUnderControl.Modules.Transactions.Application.Clients.Files.DTO;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Clients.Files.Requests;
using BudgetUnderControl.Shared.Abstractions.Modules;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Clients.Files
{
    public class FilesApiClient : IFilesApiClient
    {
        private readonly IModuleClient _client;

        public FilesApiClient(IModuleClient client)
        {
            _client = client;
        }
        
        public async Task<byte[]> GetFileContentAsync(Guid id)
            => await _client.SendAsync<byte[]>("files/getContent", new GetFileContent(id));

        public async Task<List<FileDto>> GetFilesAsync(Guid userId, DateTime changedSince)
            => await _client.SendAsync<List<FileDto>>("files/getMany",
                new GetFiles(userId, changedSince));

        public async Task DeleteFileAsync(Guid id)
            => await _client.SendAsync("files/delete", new DeleteFile(id));

        public async Task CreateFileAsync(CreateFileRequest request)
            => await _client.SendAsync("files/create", request);

    }
}
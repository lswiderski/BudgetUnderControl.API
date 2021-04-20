using System.Threading;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Files.Core.DTO;
using BudgetUnderControl.Modules.Files.Core.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;

namespace BudgetUnderControl.Modules.Files.Core.Commands.Files.GetFile
{
    internal class GetFileQueryHandler : IQueryHandler<GetFileQuery, FileDto>
    {
        private readonly IFileService _fileService;

        internal GetFileQueryHandler(IFileService fileService)
        {
            this._fileService = fileService;
        }
        public async Task<FileDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var backup = await this._fileService.GetFileAsync(request.FileId, request.Token);
            return backup;
        }
    }
}
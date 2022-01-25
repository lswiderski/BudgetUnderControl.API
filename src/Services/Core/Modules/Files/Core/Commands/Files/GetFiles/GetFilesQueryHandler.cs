using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Files.Core.DTO;
using BudgetUnderControl.Modules.Files.Core.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;

namespace BudgetUnderControl.Modules.Files.Core.Commands.Files.GetFile
{
    internal class GetFilesQueryHandler : IQueryHandler<GetFilesQuery, List<FileDto>>
    {
        private readonly IFileService _fileService;

        internal GetFilesQueryHandler(IFileService fileService)
        {
            this._fileService = fileService;
        }
        public async Task<List<FileDto>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            var backup = await this._fileService.GetFilesAsync(request.UserId, request.ChangedSince);
            return backup;
        }
    }
}
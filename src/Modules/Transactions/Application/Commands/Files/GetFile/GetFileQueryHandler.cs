using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Files.GetFile
{
    internal class GetFileQueryHandler : IQueryHandler<GetFileQuery, FileDto>
    {
        private readonly IFileService fileService;

        internal GetFileQueryHandler(IFileService fileService)
        {
            this.fileService = fileService;
        }
        public async Task<FileDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            var backup = await this.fileService.GetFileAsync(request.FileId, request.Token);
            return backup;
        }
    }
}
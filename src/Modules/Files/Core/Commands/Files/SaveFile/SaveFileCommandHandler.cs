using System;
using System.Threading;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Files.Core.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;

namespace BudgetUnderControl.Modules.Files.Core.Commands.Files.SaveFile
{
    internal class SaveFileCommandHandler : ICommandHandler<SaveFileCommand, Guid>
    {
        private readonly IFileService _fileService;

        internal SaveFileCommandHandler(IFileService fileService)
        {
            this._fileService = fileService;
        }

        public async Task<Guid> Handle(SaveFileCommand request, CancellationToken cancellationToken)
        {
            var fileId = await this._fileService.SaveFileAsync(request.File);

            return fileId;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Files.Core.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using MediatR;

namespace BudgetUnderControl.Modules.Files.Core.Commands.Files.DeleteFile
{
    internal class DeleteFileCommandHandler : ICommandHandler<DeleteFileCommand>
    {
        private readonly IFileService _fileService;

        internal DeleteFileCommandHandler(IFileService fileService)
        {
            this._fileService = fileService;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            await this._fileService.RemoveFileAsync(request.FileId);

            return Unit.Value;
        }
    }
}

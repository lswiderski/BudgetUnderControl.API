﻿using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Files.DeleteFile
{
    internal class DeleteFileCommandHandler : ICommandHandler<DeleteFileCommand>
    {
        private readonly IFileService fileService;

        internal DeleteFileCommandHandler(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            await this.fileService.RemoveFileAsync(request.FileId);

            return Unit.Value;
        }
    }
}

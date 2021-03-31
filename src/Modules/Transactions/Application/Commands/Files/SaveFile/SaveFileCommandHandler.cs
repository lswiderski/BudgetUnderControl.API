using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Files.SaveFile
{
    internal class SaveFileCommandHandler : ICommandHandler<SaveFileCommand, Guid>
    {
        private readonly IFileService fileService;

        internal SaveFileCommandHandler(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public async Task<Guid> Handle(SaveFileCommand request, CancellationToken cancellationToken)
        {
            var fileId = await this.fileService.SaveFileAsync(request.File);

            return fileId;
        }
    }
}

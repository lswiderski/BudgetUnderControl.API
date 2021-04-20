using System;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;

namespace BudgetUnderControl.Modules.Files.Core.Commands.Files.DeleteFile
{
    public class DeleteFileCommand : CommandBase
    {
        public DeleteFileCommand(Guid fileId)
        {
            FileId = fileId;
        }

        public Guid FileId {get;}
    }
}

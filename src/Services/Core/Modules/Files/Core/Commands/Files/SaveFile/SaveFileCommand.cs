using System;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using Microsoft.AspNetCore.Http;

namespace BudgetUnderControl.Modules.Files.Core.Commands.Files.SaveFile
{
    public class SaveFileCommand : CommandBase<Guid>
    {
        public SaveFileCommand(IFormFile file)
        {
            File = file;
        }
        public IFormFile File { get; }
    }
}

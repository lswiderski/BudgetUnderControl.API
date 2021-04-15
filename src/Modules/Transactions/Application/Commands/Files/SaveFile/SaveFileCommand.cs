using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Files.SaveFile
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

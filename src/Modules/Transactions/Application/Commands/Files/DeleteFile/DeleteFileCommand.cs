using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Files.DeleteFile
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

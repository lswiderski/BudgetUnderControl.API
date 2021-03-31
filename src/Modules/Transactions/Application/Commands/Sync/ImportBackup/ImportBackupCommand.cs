using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Sync.ImportBackup
{
    public class ImportBackupCommand : CommandBase
    {
        public ImportBackupCommand(BackUpDTO backup)
        {
            Backup = backup;
        }

        public BackUpDTO Backup { get; }
    }
}

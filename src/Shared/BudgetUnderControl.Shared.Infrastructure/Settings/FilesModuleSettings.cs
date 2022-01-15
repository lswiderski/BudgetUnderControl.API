using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Shared.Infrastructure.Settings
{
    public class FilesModuleSettings
    {
        public string FileRootPath { get; set; }

        public DatabaseConfig Database { get; set; }
    }
}

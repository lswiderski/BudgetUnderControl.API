using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Shared.Infrastructure.Settings
{
    public class EmailModuleSettings
    {
        public SmtpClientConfig SmtpClient { get; set; }
        public string FrontEndHostBaseURL { get; set; }
    }
}

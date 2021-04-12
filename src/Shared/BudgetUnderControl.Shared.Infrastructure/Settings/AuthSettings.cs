using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Shared.Infrastructure.Settings
{
    public class AuthSettings
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public int JWTExpiresDays { get; set; }
    }
}

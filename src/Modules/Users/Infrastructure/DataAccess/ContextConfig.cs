using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain
{
    public class ContextConfig : IContextConfig
    {
        private string connectionString;
        public string ConnectionString { get
            {
                return this.connectionString;
            }
            set
            {
                connectionString = value;
            }
        }
    }
}

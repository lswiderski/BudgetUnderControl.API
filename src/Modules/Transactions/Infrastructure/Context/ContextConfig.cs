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
        public string DbName { get; set; }
        public string DbPath { get; set; }
        public ApplicationType Application { get; set; }
        private string connectionString;
        public string ConnectionString { get
            {
                switch (this.Application)
                {
                    case ApplicationType.Mobile:
                    case ApplicationType.SQLiteMigrations:
                        return string.Format("{0}{1}", this.connectionString, this.DbPath);
                    case ApplicationType.SqlServerMigrations:
                    case ApplicationType.Web:
                    case ApplicationType.Test:
                        return this.connectionString;
                        
                    default:
                        return string.Empty;
                }
            }
            set
            {
                connectionString = value;
            }
        }
    }
}

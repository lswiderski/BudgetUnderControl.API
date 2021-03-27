using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain
{
    public interface IContextConfig
    {
        string DbName { get; set; }
        string DbPath { get; set; }
        ApplicationType Application { get; set; }
        string ConnectionString { get;}
    }
}

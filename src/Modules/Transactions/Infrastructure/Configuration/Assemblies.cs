using BudgetUnderControl.Modules.Transactions.Application.Configuration;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase<>).Assembly;

        public static readonly Assembly Infrastructure = typeof(TransactionsModuleExecutor).Assembly;
    }
}

using BudgetUnderControl.Modules.Transactions.Application.Transactions.AddTransaction;
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
        public static readonly Assembly Application = typeof(AddTransactionCommand).Assembly;

        public static readonly Assembly Infrastructure = typeof(TransactionsModuleExecutor).Assembly;
    }
}

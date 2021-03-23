using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Reports.GetMovingSum
{
    public class GetMovingSumQuery : QueryBase<List<MovingSumItemDTO>>
    {
        public GetMovingSumQuery(TransactionsFilter filters)
        {
            Filters = filters;
        }

        public TransactionsFilter Filters { get; }
    }
}

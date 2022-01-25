using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Reports.GetMovingSum
{
    public class GetMovingSumQuery : QueryBase<List<MovingSumItemDTO>>
    {
        public GetMovingSumQuery(TransactionsFilterDTO filters)
        {
            Filters = filters;
        }

        public TransactionsFilterDTO Filters { get; }
    }
}

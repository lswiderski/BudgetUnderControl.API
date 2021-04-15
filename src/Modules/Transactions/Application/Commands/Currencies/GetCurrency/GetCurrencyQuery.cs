using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Currencies.GetCurrency
{
    public class GetCurrencyQuery : QueryBase<CurrencyDTO>
    {
        public GetCurrencyQuery(int currencyId)
        {
            CurencyId = currencyId;
        }

        public int CurencyId { get; }
    }
}

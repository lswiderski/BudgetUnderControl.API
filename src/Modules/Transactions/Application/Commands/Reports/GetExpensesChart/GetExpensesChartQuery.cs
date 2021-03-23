﻿using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Reports.GetExpensesChart
{
    public class GetExpensesChartQuery : QueryBase<List<ExpensesColumnChartSeriesDto>>
    {
        public GetExpensesChartQuery(TransactionsFilter filters)
        {
            Filters = filters;
        }

        public TransactionsFilter Filters { get; }
    }
}

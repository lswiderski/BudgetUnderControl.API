using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Contracts
{
    public class ExpensesColumnChartSeriesDto
    {
        public List<ExpensesColumnChartItemDto> Data { get; set; }

        public string Name { get; set; }
    }
}

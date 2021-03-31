using System.Collections.Generic;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class ExpensesColumnChartSeriesDto
    {
        public List<ExpensesColumnChartItemDto> Data { get; set; }

        public string Name { get; set; }
    }
}

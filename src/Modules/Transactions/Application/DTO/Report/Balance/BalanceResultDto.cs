using System.Collections.Generic;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO.Report.Balance
{
    public class BalanceResultDto
    {
        public List<BalanceDto> Incomes { get; set; }

        public List<BalanceDto> Expenses { get; set; }

        public List<BalanceDto> Total { get; set; }
    }
}
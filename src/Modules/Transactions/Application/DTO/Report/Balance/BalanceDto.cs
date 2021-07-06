namespace BudgetUnderControl.Modules.Transactions.Application.DTO.Report.Balance
{
    public class BalanceDto
    {
        public decimal Value { get; set; }

        public string Currency { get; set; }

        public bool IsExchanged { get; set; }
    }
}
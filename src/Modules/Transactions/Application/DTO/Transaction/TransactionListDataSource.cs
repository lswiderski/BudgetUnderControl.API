using System.Collections.Generic;
using System.Linq;
using BudgetUnderControl.Modules.Transactions.Application.DTO.Report.Balance;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO.Transaction
{
    public class TransactionListDataSource
    {
        public ICollection<TransactionListItemDTO> Transactions { get; set; }

        public int NumberOfTransactions { get; init; }

        public BalanceResultDto Balance { get; init; }

        public BalanceDto ExpenseInMainCurrency
        {
            get
            {
                return Balance.Expenses.Where(x => x.IsExchanged).FirstOrDefault();
            }
        } 
        
        public List<BalanceDto> ExpensesInOriginalCurrencies
        {
            get
            {
                return Balance.Expenses.Where(x => !x.IsExchanged).ToList();
            }
        } 
        
        public BalanceDto IncomeInMainCurrency
        {
            get
            {
                return Balance.Incomes.Where(x => x.IsExchanged).FirstOrDefault();
            }
        } 
        
        public List<BalanceDto> IncomesInOriginalCurrencies
        {
            get
            {
                return Balance.Incomes.Where(x => !x.IsExchanged).ToList();
            }
        } 

        public TransactionListDataSource()
        {
            this.Transactions = new List<TransactionListItemDTO>();
        }
    }
}
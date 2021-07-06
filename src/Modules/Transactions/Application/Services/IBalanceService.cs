using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.DTO.Report.Balance;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface IBalanceService
    {
        Task<BalanceResultDto> GetBalanceAsync(ICollection<TransactionListItemDTO> transactions);

        Task<BalanceResultDto> GetBalanceAsync(ICollection<TransactionListItemDTO> transactions, string mainCurrency);

        Task<BalanceResultDto> GetBalanceAsync(TransactionsFilterDTO filter, string mainCurrency);

        Task<BalanceResultDto> GetTotalCurrentBalanceAsync();
    }
}
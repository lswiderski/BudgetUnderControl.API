using BudgetUnderControl.Common;
using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface ITransactionService
    {
        Task<ICollection<TransactionListItemDTO>> GetTransactionsAsync(TransactionsFilter filter = null);
        Task<EditTransactionDTO> GetTransactionAsync(Guid transactionId);
        Task EditTransactionAsync(EditTransaction command);
        Task AddTransactionAsync(AddTransaction command);
        Task DeleteTransactionAsync(DeleteTransaction command);
        Task CreateTagsToTransaction(IEnumerable<int> tagsId, int transactionId);
    }
}

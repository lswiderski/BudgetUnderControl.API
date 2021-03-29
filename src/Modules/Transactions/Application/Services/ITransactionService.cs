using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface ITransactionService
    {
        Task<ICollection<TransactionListItemDTO>> GetTransactionsAsync(TransactionsFilterDTO filter = null);
        Task<EditTransactionDTO> GetTransactionAsync(Guid transactionId);
        Task EditTransactionAsync(EditTransaction command);
        Task AddTransactionAsync(AddTransaction command);
        Task DeleteTransactionAsync(Guid transactionId);
        Task CreateTagsToTransaction(IEnumerable<int> tagsId, int transactionId);
    }
}

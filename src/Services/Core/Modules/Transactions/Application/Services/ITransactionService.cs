using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.AddTransaction;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.EditTransaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.DTO.Transaction;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface ITransactionService
    {
        Task<ICollection<TransactionListItemDTO>> GetTransactionsAsync(TransactionsFilterDTO filter = null);
        Task<ICollection<TransactionExportItemDto>> GetTransactionsToExportAsync(TransactionsFilterDTO filter = null);
        Task<EditTransactionDTO> GetTransactionAsync(Guid transactionId);
        Task EditTransactionAsync(EditTransactionCommand command);
        Task AddTransactionAsync(AddTransactionCommand command);
        Task DeleteTransactionAsync(Guid transactionId);
        Task CreateTagsToTransaction(IEnumerable<int> tagsId, int transactionId);
    }
}

using BudgetUnderControl.Common;
using BudgetUnderControl.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain.Repositiories
{
    public interface ITransactionRepository
    {
        Task<ICollection<Transaction>> GetTransactionsAsync(TransactionsFilter filter = null);
        Task<ICollection<Transfer>> GetTransfersAsync();
        Task<Transaction> GetTransactionAsync(int id);
        Task<Transaction> GetTransactionAsync(Guid id);
        Task AddTransactionAsync(Transaction transaction);
        Task AddTransactionsAsync(IEnumerable<Transaction> transactions);
        Task UpdateAsync(Transaction transaction);
        Task UpdateAsync(IEnumerable<Transaction> transactions);
        Task AddTransferAsync(Transfer transfer);
        Task UpdateTransferAsync(Transfer transfer);
        Task RemoveTransactionAsync(Transaction transaction);
        Task RemoveTransferAsync(Transfer transfer);
        Task<Transfer> GetTransferAsync(int transactionId);
        Task<Transfer> GetTransferAsync(Guid transactionId);
        Task HardRemoveTransactionsAsync(IEnumerable<Transaction> transactions);
        Task HardRemoveTransfersAsync(IEnumerable<Transfer> transfers);
        Task<ICollection<Transfer>> GetTransfersModifiedSinceAsync(DateTime changedSince);
    }
}

using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.DeleteTransaction
{
    public class DeleteTransactionCommand : CommandBase
    {
        public DeleteTransactionCommand(Guid transactionId)
        {
            TransactionId = transactionId;
        }
        public Guid TransactionId { get; }
    }
}

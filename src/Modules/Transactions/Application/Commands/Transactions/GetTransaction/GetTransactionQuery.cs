﻿using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.GetTransaction
{
    public class GetTransactionQuery : QueryBase<EditTransactionDTO>
    {
        public GetTransactionQuery(Guid transactionId)
        {
            TransactionId = transactionId;
        }

        public Guid TransactionId { get; }
    }

    
}

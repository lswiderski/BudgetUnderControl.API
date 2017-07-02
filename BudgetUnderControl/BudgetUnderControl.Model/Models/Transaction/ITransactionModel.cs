﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Model
{
    public interface ITransactionModel
    {
        void AddTransaction(AddTransactionDTO arg);
        void AddTransfer(AddTransferDTO args);
        Task<ICollection<TransactionListItemDTO>> GetTransactions();
        Task<ICollection<TransactionListItemDTO>> GetTransactions(int accountId);
    }
}

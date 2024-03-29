﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Modules.Transactions.Core.ValueObjects
{
    public class TransactionsFilter
    {
        public IEnumerable<int> AccountsIds { get; set; }
        public IEnumerable<Guid> AccountsExternalIds { get; set; }
        public IEnumerable<int?> CategoryIds { get; set; }
        public IEnumerable<int> TagIds { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IncludeDeleted { get; set; }
        public DateTime? ChangedSince { get; set; }
        public string SearchQuery { get; set; }
        public bool IncludeTransfers { get; set; }

        public TransactionsFilter()
        {
            this.IncludeTransfers = true;
        }
    }
}

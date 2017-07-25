﻿using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Model
{
    public class TransactionSyncDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

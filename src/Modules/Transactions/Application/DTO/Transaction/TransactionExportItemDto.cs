using System;
using System.Collections.Generic;
using BudgetUnderControl.Common.Enums;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO.Transaction
{
    public record TransactionExportItemDto
    {
        public Guid TransactionId { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public Decimal Value { get; set; }
        
        public Decimal ValueInMainCurrency { get; set; }

        public string Currency { get; set; }

        public string CategoryName { get; set; }

        public string AccountName { get; set; }

        public string Comment { get; set; }

        public List<string> Tags { get; set; }

        public TransactionType Type { get; set; }

        public bool IsTransfer { get; set; }
    }
}
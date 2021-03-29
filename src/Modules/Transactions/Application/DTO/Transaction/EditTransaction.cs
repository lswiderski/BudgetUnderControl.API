using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class EditTransaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int? CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public ExtendedTransactionType ExtendedType { get; set; }
        public int? TransferTransactionId { get; set; }
        public int? TransferId { get; set; }
        public int? TransferAccountId { get; set; }
        public DateTime? TransferDate { get; set; }
        public decimal? TransferAmount { get; set; }
        public decimal? Rate { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsDeleted { get; set; }
        public List<int> Tags { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string FileGuid { get; set; }
    }
}

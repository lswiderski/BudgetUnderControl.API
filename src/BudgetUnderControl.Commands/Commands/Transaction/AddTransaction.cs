using BudgetUnderControl.Common.Enums;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class AddTransaction
    {
        public int AccountId { get; set; }
        public int? CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public ExtendedTransactionType Type { get; set; }
        public int? TransferAccountId { get; set; }
        public DateTime TransferDate { get; set; }
        public decimal TransferAmount { get; set; }
        public decimal Rate { get; set; }
        public Guid ExternalId { get; }
        public Guid TransferExternalId { get; }
        public List<int> Tags { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string FileGuid { get; set; }

        public AddTransaction()
        {
            this.ExternalId = Guid.NewGuid();
            this.TransferExternalId = Guid.NewGuid();
        }
    }
}

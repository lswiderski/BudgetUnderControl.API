using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.AddTransaction
{
    public class AddTransactionCommand : CommandBase<Guid>
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
        public List<int> Tags { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string FileGuid { get; set; }

        public Guid ExternalId { get; }
        public Guid TransferExternalId { get; }

        public AddTransactionCommand()
        {
            this.ExternalId = Guid.NewGuid();
            this.TransferExternalId = Guid.NewGuid();
        }
    }
}

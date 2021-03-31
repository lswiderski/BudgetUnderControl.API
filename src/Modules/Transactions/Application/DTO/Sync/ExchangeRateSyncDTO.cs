using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class ExchangeRateSyncDTO
    {
        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }

        public double Rate { get; set; }

        public DateTime Date { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

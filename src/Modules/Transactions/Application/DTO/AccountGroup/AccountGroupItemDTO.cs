using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class AccountGroupItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ExternalId { get; set; }
    }
}

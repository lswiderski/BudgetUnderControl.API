using System;

namespace BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsDeleted { get;  set; }
    }
}
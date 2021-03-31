using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class FileSyncDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

﻿using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class AccountGroupSyncDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsDeleted { get; set; }

        public Guid OwnerExternalId { get; set; }
    }
}

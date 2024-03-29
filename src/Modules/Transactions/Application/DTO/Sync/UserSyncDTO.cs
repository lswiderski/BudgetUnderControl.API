﻿using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class UserSyncDTO
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

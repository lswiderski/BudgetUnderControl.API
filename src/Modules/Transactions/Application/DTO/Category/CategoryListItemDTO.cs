﻿using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class CategoryListItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ExternalId { get; set; }

        public string Icon { get; set; }
    }
}
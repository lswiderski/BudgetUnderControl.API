﻿using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class TransactionListItemDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Account { get; set; }
        public TransactionType Type { get; set; }
        public decimal Value { get; set; }
        public decimal ValueInMainCurrency { get; set; }
        public string ValueWithCurrency { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public bool? IsTransfer { get; set; }
        public Guid ExternalId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<TagDTO> Tags {get;set;}
        public int? CategoryId { get; set; }
        public string Category { get; set; }
        public DateTime JustDate {
            get
            {
                return Date.Date;
            }
        }

        public TransactionListItemDTO()
        {
            this.Tags = new List<TagDTO>();
        }
    }
}

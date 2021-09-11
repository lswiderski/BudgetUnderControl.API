using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Account { get; set; }
        public byte Type { get; set; }
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
        public List<TagDTO> Tags { get; set; }

        public string TagsJoined
        {
            get { return string.Join(", ", Tags.Select(x => x.Name)); }
        }

        public int? CategoryId { get; set; }
        public string Category { get; set; }

        public DateTime JustDate
        {
            get { return Date.Date; }
        }

        public TransactionDTO()
        {
            this.Tags = new List<TagDTO>();
        }
    }
}
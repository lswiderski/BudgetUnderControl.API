using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Common.Contracts
{
    public class TransactionSyncDTO
    {
        public int Id { get; set; }
        public Guid? ExternalId { get; set; }
        public int AccountId { get; set; }
        public Guid? AccountExternalId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int? CategoryId { get; set; }
        public Guid? CategoryExternalId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public List<TagSyncDTO> Tags { get; set; }
        public List<FileToTransactionSyncDTO> Files { get; set; }

        public TransactionSyncDTO()
        {
            this.Tags = new List<TagSyncDTO>();
            this.Files = new List<FileToTransactionSyncDTO>();
        }
    }
}

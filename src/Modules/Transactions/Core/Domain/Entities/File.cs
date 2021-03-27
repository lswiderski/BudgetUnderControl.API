using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BudgetUnderControl.Domain
{
    public class File : ISyncable
    {
        [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ContentType { get; set; }

        public ICollection<FileToTransaction> FileToTransactions { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Guid ExternalId { get; set; }

        public bool IsDeleted { get; set; }

        public void UpdateModify()
        {
            this.ModifiedOn = DateTime.UtcNow;
        }

        public void SetModifiedOn(DateTime? date)
        {
            this.ModifiedOn = date;
        }

        public void Delete(bool delete = true)
        {
            if(delete != this.IsDeleted)
            {
                this.IsDeleted = delete;
                this.UpdateModify();
            }
        }
    }
}

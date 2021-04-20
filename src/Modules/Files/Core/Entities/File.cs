using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetUnderControl.Modules.Files.Core.Entities
{
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ContentType { get; set; }

        public DateTime? ModifiedOn { get; set; }

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
            if (delete == this.IsDeleted) return;
            this.IsDeleted = delete;
            this.UpdateModify();
        }
    }
}

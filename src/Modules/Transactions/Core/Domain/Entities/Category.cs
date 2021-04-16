using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain
{
    public class Category : ISyncable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string Icon { get; set; }

        public Guid UserId { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
        public Guid ExternalId { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public List<Transaction> Transactions { get; set; }

        [NotMapped]
        public string ExternalIdString
        {
            get
            {
                return this.ExternalId.ToString();
            }
        }

        public static Category Create(string name, Guid ownerId, Guid? externalId, string icon = null)
        {
            return new Category
            {
                Name = name,
                UserId = ownerId,
                ExternalId = externalId ?? Guid.NewGuid(),
                ModifiedOn = DateTime.UtcNow,
                IsDeleted = false,
                IsDefault = false,
                Icon = icon,
            };
        }

        public void Edit(string name, Guid ownerId, string icon = null)
        {
            this.Name = name;
            this.UserId = ownerId;
            this.Icon = icon;
            this.UpdateModify();
        }

        public void Delete(bool delete = true)
        {
            this.IsDeleted = delete;
            this.UpdateModify();
        }

        public void SetModifiedOn(DateTime? dateTime)
        {
            this.ModifiedOn = dateTime;
        }

        public void UpdateModify()
        {
            this.ModifiedOn = DateTime.UtcNow;
        }
    }
}

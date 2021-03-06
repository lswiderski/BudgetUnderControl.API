﻿using System;
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

        public int OwnerId { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
        public Guid ExternalId { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public virtual User Owner { get; set; }
        public List<Transaction> Transactions { get; set; }

        [NotMapped]
        public string ExternalIdString
        {
            get
            {
                return this.ExternalId.ToString();
            }
        }

        public static Category Create(string name, int ownerId, Guid? externalId)
        {
            return new Category
            {
                Name = name,
                OwnerId = ownerId,
                ExternalId = externalId ?? Guid.NewGuid(),
                ModifiedOn = DateTime.UtcNow,
                IsDeleted = false,
                IsDefault = false
            };
        }

        public void Edit(string name, int ownerId)
        {
            this.Name = name;
            this.OwnerId = ownerId;
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

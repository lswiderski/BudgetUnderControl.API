using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain
{
    public class Account : ISyncable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        [StringLength(250)]
        public string Name { get; protected set; }
        public int CurrencyId { get; protected set; }
        public bool IsIncludedToTotal { get; protected set; }
        public string Comment { get; protected set; }
        public int Order { get; protected set; }
        public AccountType Type { get; protected set; }
        public int? ParentAccountId { get; protected set; }
        public string Icon { get; set; }

        public bool IsActive { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
        public Guid ExternalId { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public Currency Currency { get;  set; }
        public List<AccountSnapshot> AccountSnapshots { get; protected set; }
        public List<Transaction> Transactions { get; protected set; }


        protected Account()
        {

        }

        public static Account Create(string name, int currencyId, 
            bool isIncludedToTotal, string comment, int order, AccountType type, 
            int? parentAccountId, bool isActive, bool isDeleted, Guid ownerId, Guid? externalId = null, string icon = null)
        {
            return new Account()
            {
                Name = name,
                CurrencyId = currencyId,
                IsActive = isActive,
                IsIncludedToTotal = isIncludedToTotal,
                Comment = comment,
                Order = order,
                Type = type,
                ParentAccountId = parentAccountId,
                ExternalId = externalId ?? Guid.NewGuid(),
                UserId = ownerId,
                ModifiedOn = DateTime.UtcNow,
                IsDeleted = isDeleted,
                Icon = icon,
            };
        }

        public void Edit(string name, int currencyId,
            bool isIncludedToTotal, string comment, int order, AccountType type,
            int? parentAccountId, bool isActive, bool? isDeleted = null, Guid? ownerId = null, string icon = null )
        {
            this.Name = name;
            this.CurrencyId = currencyId;
            this.IsActive = isActive;
            this.IsIncludedToTotal = isIncludedToTotal;
            this.Comment = comment;
            this.Order = order;
            this.Type = type;
            this.ParentAccountId = parentAccountId;
           
            this.Icon = icon;
            if(ownerId != null)
            {
                this.UserId = ownerId.Value;
            }
            if(isDeleted.HasValue)
            {
                this.IsDeleted = isDeleted.Value;
            }

            this.UpdateModify();
        }

        /// <summary>
        /// Use for sync/imports
        /// </summary>
        /// <param name="id"></param>
        public void SetId(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Use for sync/imports
        /// </summary>
        /// <param name="id"></param>
        public void SetParentAccountId(int? id)
        {
            this.ParentAccountId = id;
        }

        /// <summary>
        /// Use for sync/imports
        /// </summary>
        /// <param name="id"></param>
        public void SetActive(bool isActive)
        {
            this.IsActive = isActive;
            this.IsDeleted = !isActive;
            this.UpdateModify();
        }

        public void Delete(bool delete = true)
        {
            this.SetActive(!delete);
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

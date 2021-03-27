using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain
{
    public class Transfer : ISyncable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        public int FromTransactionId { get; protected set; }
        public int ToTransactionId { get; protected set; }
        public decimal Rate { get; protected set; }

        public DateTime? ModifiedOn { get; protected set; }
        public Guid ExternalId { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public Transaction FromTransaction { get; set; }
        public Transaction ToTransaction { get; set; }

      

        public Transfer()
        {

        }

        public static Transfer Create(int fromTransactionId, int toTransactionId, decimal rate, Guid? guid = null)
        {
            return new Transfer()
            {
                FromTransactionId = fromTransactionId,
                ToTransactionId = toTransactionId,
                Rate = rate,
                ExternalId = guid ?? Guid.NewGuid(),
                ModifiedOn = DateTime.UtcNow,
                IsDeleted = false,
            };
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
        /// <param name="rate"></param>
        public void SetRate(decimal rate)
        {
            this.Rate = rate;
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

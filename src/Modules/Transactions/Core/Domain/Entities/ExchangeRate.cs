using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain
{
    public class ExchangeRate : ISyncable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }

        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Guid ExternalId { get; set; }

        public int? UserId { get; set; }

        public bool IsDeleted { get; set; }

        protected ExchangeRate()
        {

        }

        public static ExchangeRate Create(int fromCurrencyId, int toCurrencyId, double rate, int userId, Guid? externalId, bool isDeleted = false, DateTime? date = null)
        {
            return new ExchangeRate
            {
                FromCurrencyId = fromCurrencyId,
                ToCurrencyId = toCurrencyId,
                Rate = rate,
                Date = date ?? DateTime.UtcNow,
                ExternalId = externalId ?? Guid.NewGuid(),
                IsDeleted = isDeleted,
                UserId = userId
            };
        }

        public void Edit(int fromCurrencyId, int toCurrencyId, double rate, DateTime date, bool isDeleted)
        {
            this.Rate = rate;
            this.Date = date;
            this.FromCurrencyId = fromCurrencyId;
            this.ToCurrencyId = toCurrencyId;
            this.IsDeleted = isDeleted;
        }

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
            this.IsDeleted = delete;
            this.UpdateModify();
        }
    }
}

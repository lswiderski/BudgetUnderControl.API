using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; protected set; }
        [StringLength(3)]
        public string Code { get; protected set; }
        [StringLength(250)]
        public string FullName { get; protected set; }
        public short Number { get; protected set; }
        [StringLength(3)]
        public string Symbol { get; protected set; }

        public List<Account> Accounts { get; set; }
        public List<ExchangeRate> FromExchangeRates { get; set; }
        public List<ExchangeRate> ToExchangeRates { get; set; }

        public Currency()
        {

        }

        public static Currency Create(string code, string name, short number, string symbol)
        {
            return new Currency
            {
                Code = code,
                FullName = name,
                Number = number,
                Symbol = symbol
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
    }
}

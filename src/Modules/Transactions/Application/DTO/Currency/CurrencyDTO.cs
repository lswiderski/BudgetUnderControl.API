using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Common.Contracts
{
    public class CurrencyDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public short Number { get; set; }
        public string Symbol { get; set; }

        public string CodeWithName
        {
            get
            {
                return string.Format("{0} - {1}", this.Code, this.Name);
            }
        }
    }
}

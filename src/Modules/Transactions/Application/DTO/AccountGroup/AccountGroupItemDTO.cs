using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Common.Contracts
{
    public class AccountGroupItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid ExternalId { get; set; }
    }
}

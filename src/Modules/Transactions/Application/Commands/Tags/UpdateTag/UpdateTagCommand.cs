using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.UpdateTag
{
    public class UpdateTagCommand : CommandBase
    {
        public string Name { get; set; }

        public Guid ExternalId { get; set; }

        public bool IsDeleted { get; set; }
    }
}

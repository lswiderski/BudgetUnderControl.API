using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.CreateTag
{
    public class CreateTagCommand : CommandBase<Guid>
    {
        public string Name { get; set; }

        public Guid ExternalId { get; }

        public CreateTagCommand()
        {
            this.ExternalId = Guid.NewGuid();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class AddTag
    {
        public string Name { get; set; }

        public Guid ExternalId { get; set; }

        public AddTag()
        {
            this.ExternalId = Guid.NewGuid();
        }
    }
}

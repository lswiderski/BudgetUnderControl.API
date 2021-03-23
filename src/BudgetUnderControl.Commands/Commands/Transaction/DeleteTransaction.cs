using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class DeleteTransaction
    {
        public int? Id { get; set; }

        public Guid? ExternalId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class ActivateUserCommand 
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class CommandResult : ICommandResult
    {
        public bool IsSuccess { get; set; }
    }
}

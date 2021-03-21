using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    [Obsolete]
    public class CommandResult : ICommandResult
    {
        public bool IsSuccess { get; set; }
    }
}

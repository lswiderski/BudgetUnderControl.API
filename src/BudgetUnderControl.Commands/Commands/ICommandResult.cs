using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    [Obsolete]
    public interface ICommandResult
    {
        bool IsSuccess { get; }
    }
}

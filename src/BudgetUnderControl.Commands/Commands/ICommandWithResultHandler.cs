using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    [Obsolete]
    public interface ICommandWithResultHandler<T> where T : ICommand
    {
        Task<ICommandResult> HandleAsync(T command);
    }
}

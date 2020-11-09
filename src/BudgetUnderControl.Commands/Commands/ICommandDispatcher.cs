using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
        Task DispatchAsync<T>(T command, IComponentContext context) where T : ICommand;

        Task<ICommandResult> DispatchWithResultAsync<T>(T command) where T : ICommand;
        Task<ICommandResult> DispatchWithResultAsync<T>(T command, IComponentContext context) where T : ICommand;

    }

}

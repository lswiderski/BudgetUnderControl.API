using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext context;

        public CommandDispatcher(IComponentContext context)
        {
            this.context = context.Resolve<IComponentContext>();
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
             await this.DispatchAsync(command, context);
        }

        public async Task DispatchAsync<T>(T command, IComponentContext _context) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command),
                    $"Command: '{typeof(T).Name}' cannot be null.");
            }

            var handler = _context.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command);
        }

        public async Task<ICommandResult> DispatchWithResultAsync<T>(T command) where T : ICommand
        {
             return await this.DispatchWithResultAsync(command, context);
        }

        public async Task<ICommandResult> DispatchWithResultAsync<T>(T command, IComponentContext _context) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command),
                    $"Command: '{typeof(T).Name}' cannot be null.");
            }

            var handler = _context.Resolve<ICommandWithResultHandler<T>>();
            var result = await handler.HandleAsync(command);

            return result;
        }
    }
}

using System.Threading.Tasks;
using Autofac;
using BudgetUnderControl.Modules.Notifications.Core.Configuration;
using BudgetUnderControl.Modules.Notifications.Core.Configuration.Processing;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using MediatR;

namespace BudgetUnderControl.Modules.Notifications.Core
{
    public class NotificationsModuleExecutor: INotificationsModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await CommandsExecutor.Execute(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await CommandsExecutor.Execute(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = NotificationsCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}
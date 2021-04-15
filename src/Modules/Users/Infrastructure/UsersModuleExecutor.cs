using Autofac;
using BudgetUnderControl.Modules.Users.Application.Contracts;
using BudgetUnderControl.Modules.Users.Infrastructure.Configuration;
using BudgetUnderControl.Modules.Users.Infrastructure.Configuration.Processing;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Infrastructure
{
    public class UsersModuleExecutor : IUsersModule
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
            using (var scope = UsersCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}

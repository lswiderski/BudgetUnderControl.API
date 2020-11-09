using BudgetUnderControl.CommonInfrastructure.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetUnderControl.API.Controllers
{
    public class ApiControllerBase : Controller
    {
        private readonly ICommandDispatcher CommandDispatcher;

        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            await CommandDispatcher.DispatchAsync(command);
        }

        protected async Task<ICommandResult> DispatchWithResultAsync<T>(T command) where T : ICommand
        {
            return await CommandDispatcher.DispatchWithResultAsync(command);
        }
    }
}

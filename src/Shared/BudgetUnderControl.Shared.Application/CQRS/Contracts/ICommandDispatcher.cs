﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Shared.Application.CQRS.Contracts
{
    public interface ICommandDispatcher
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}

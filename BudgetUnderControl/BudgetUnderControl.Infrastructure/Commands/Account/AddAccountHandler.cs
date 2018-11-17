﻿using BudgetUnderControl.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Infrastructure.Commands
{
    public class AddAccountHandler : ICommandHandler<AddAccount>
    {
        private readonly IAccountService accountService;
        public AddAccountHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task HandleAsync(AddAccount command)
        {
            await this.accountService.AddAccountAsync(command);

        }
    }
}

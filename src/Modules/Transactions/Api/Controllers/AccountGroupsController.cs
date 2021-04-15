using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Commands.AccountGroups.GetAccountGroup;
using BudgetUnderControl.Modules.Transactions.Application.Commands.AccountGroups.GetAllAccountGroups;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;

namespace BudgetUnderControl.Modules.Transactions.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    class AccountGroupsController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;

        public AccountGroupsController(ITransactionsModule transactionsModule)
        {
            _transactionsModule = transactionsModule;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AccountGroupItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var accounts = await this._transactionsModule.ExecuteQueryAsync(new GetAllAccountGroupsQuery());
            return Ok(accounts);
        }

        // GET api/currencies/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountGroupItemDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var account = await this._transactionsModule.ExecuteQueryAsync(new GetAccountGroupQuery(id));
            return Ok(account);
        }
    }
}


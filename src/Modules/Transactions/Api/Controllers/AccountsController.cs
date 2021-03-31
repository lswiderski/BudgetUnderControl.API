using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.CreateAccount;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.DeleteAccount;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.GetAccount;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.GetAccountDetails;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.GetAccounts;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.UpdateAccount;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;

        public AccountsController(ITransactionsModule transactionsModule)
        {
            _transactionsModule = transactionsModule;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<AccountListItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var currencies = await this._transactionsModule.ExecuteQueryAsync(new GetAllAccountsQuery());
            return Ok(currencies);
        }

        // GET api/currencies/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AccountDetailsDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await this._transactionsModule.ExecuteQueryAsync(new GetAccountQuery(id));
            return Ok(category);
        }

        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(AccountDetailsDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AccountDetailsDTO>> GetAccountDetails(Guid id)
        {
            var account = await this._transactionsModule.ExecuteQueryAsync(new GetAccountDetailsQuery(id));
  
            return Ok(account);
        }

        // POST api/accounts
        [HttpPost]
        [ProducesResponseType(typeof(AccountDetailsDTO), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] CreateAccountCommand command)
        {
            var accountId = await this._transactionsModule.ExecuteCommandAsync(command);

            return CreatedAtAction(nameof(Get), new { id = accountId });
        }

        // PUT api/accounts/552cbd7c-e9d9-46c9-ab7e-2b10ae38ab4a
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateAccountCommand command)
        {
            await this._transactionsModule.ExecuteCommandAsync(command);
            return Ok();
        }

        // DELETE api/accounts/552cbd7c-e9d9-46c9-ab7e-2b10ae38ab4a
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this._transactionsModule.ExecuteCommandAsync(new DeleteAccountCommand(id));
            return NoContent();
        }
    }
}


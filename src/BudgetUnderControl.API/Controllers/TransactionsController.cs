using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure.Commands;
using BudgetUnderControl.Infrastructure.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.CommonInfrastructure;
using Microsoft.AspNetCore.Http;
using BudgetUnderControl.Common.Enums;

namespace BudgetUnderControl.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ApiControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionsController(ITransactionService transactionService, ICurrencyService currencyService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            this.transactionService = transactionService;
        }

        // GET api/transactions
        [HttpGet]
        [Authorize(Policy = UsersPolicy.AllUsers)]
        public async Task<ActionResult<IEnumerable<TransactionListItemDTO>>> Get([FromQuery] TransactionsFilter filter)
        {
            var transactions = await this.transactionService.GetTransactionsAsync(filter);
            return Ok(transactions.ToList());
        }

        // GET api/transactions/552cbd7c-e9d9-46c9-ab7e-2b10ae38ab4a
        [HttpGet("{id}")]
        [Authorize(Policy = UsersPolicy.AllUsers)]
        public async Task<ActionResult<EditTransactionDTO>> GetById(Guid id)
        {
            var transaction = await this.transactionService.GetTransactionAsync(id);
            return Ok(transaction);
        }

        // POST api/transactions
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddTransaction command)
        {
            await this.DispatchAsync(command);

            //return Created($"transactions/{command.ExternalId}", command);
            return CreatedAtAction(nameof(Get), new { id = command.ExternalId }, command);
        }

        // PUT api/transactions/552cbd7c-e9d9-46c9-ab7e-2b10ae38ab4a
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] EditTransaction command)
        {
            await this.DispatchAsync(command);
            return NoContent();
        }

        // DELETE api/transactions/552cbd7c-e9d9-46c9-ab7e-2b10ae38ab4a
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.DispatchAsync(new DeleteTransaction { ExternalId = id });
            return NoContent();
        }
    }
}

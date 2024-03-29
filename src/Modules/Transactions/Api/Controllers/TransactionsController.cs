﻿using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.AddTransaction;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.DeleteTransaction;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.EditTransaction;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.GetTransaction;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.GetTransactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.DTO.Transaction;
using BudgetUnderControl.Shared.Abstractions.Contexts;

namespace BudgetUnderControl.Modules.Transactions.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;
        private readonly IContext _context;

        public TransactionsController(ITransactionsModule transactionsModule, IContext context)
        {
            _transactionsModule = transactionsModule;
            _context = context;
        }

        // GET api/Transactions
        [HttpGet]
        [Authorize(Policy = UsersPolicy.AllUsers)]
        [ProducesResponseType(typeof(TransactionListDataSource), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TransactionListDataSource>> Get([FromQuery] TransactionsFilterDTO filter)
        {
            var transactions = await _transactionsModule.ExecuteQueryAsync(new GetTransactionsQuery(filter));
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = UsersPolicy.AllUsers)]
        [ProducesResponseType(typeof(EditTransactionDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var transaction = await _transactionsModule.ExecuteQueryAsync(new GetTransactionQuery(id));
            return Ok(transaction);

        }
        [HttpPost]
        [Authorize(Policy = UsersPolicy.AllUsers)]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] AddTransactionCommand command)
        {
            var id = await _transactionsModule.ExecuteCommandAsync(command);

            return CreatedAtAction("GetById", new { id = id }, command);
        }

        [HttpPut("{transactionId}")]
        [Authorize(Policy = UsersPolicy.AllUsers)]
        public async Task<IActionResult> Put([FromRoute] Guid transactionId, [FromBody] EditTransactionCommand command)
        {
            command.ExternalId = transactionId;
            await _transactionsModule.ExecuteCommandAsync(command);
            return Ok();
        }

        // DELETE api/transactions/552cbd7c-e9d9-46c9-ab7e-2b10ae38ab4a
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _transactionsModule.ExecuteCommandAsync(new DeleteTransactionCommand( id));
            return NoContent();
        }
    }
}

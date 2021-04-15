using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Currencies.GetAllCurrencies;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Currencies.GetCurrency;
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
    public class CurrenciesController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;

        public CurrenciesController(ITransactionsModule transactionsModule)
        {
            _transactionsModule = transactionsModule;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CurrencyDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var currencies = await this._transactionsModule.ExecuteQueryAsync(new GetAllCurrenciesQuery());
            return Ok(currencies);
        }

        // GET api/currencies/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CurrencyDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var currency = await this._transactionsModule.ExecuteQueryAsync(new GetCurrencyQuery(id));
            return Ok(currency);
        }
    }
}

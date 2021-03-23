using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.CreateTag;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.GetActiveTags;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.GetAllTags;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.GetTag;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Tags.UpdateTag;
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
    [Route("api/[controller]")]
    [ApiController]
    class TagsController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;

        public TagsController(ITransactionsModule transactionsModule)
        {
            _transactionsModule = transactionsModule;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TagDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var tags = await this._transactionsModule.ExecuteQueryAsync(new GetAllTagsQuery());
            return Ok(tags);
        }

        [HttpGet("active")]
        [ProducesResponseType(typeof(List<TagDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetActive()
        {
            var tags = await this._transactionsModule.ExecuteQueryAsync(new GetActiveTagsQuery());
            return Ok(tags);
        }

        // GET api/currencies/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TagDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tags = await this._transactionsModule.ExecuteQueryAsync(new GetTagQuery(id));
            return Ok(tags);
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTagCommand command)
        {
            var accountId = await this._transactionsModule.ExecuteCommandAsync(command);

            return CreatedAtAction(nameof(Get), new { id = accountId });
        }

        // PUT api/tags
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateTagCommand command)
        {
            await this._transactionsModule.ExecuteCommandAsync(command);
            return Ok();
        }
    }
}

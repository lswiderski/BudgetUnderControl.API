﻿using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Sync.GetBackup;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Sync.ImportBackup;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Sync.Sync;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;

namespace BudgetUnderControl.Modules.Transactions.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SyncController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;
        private readonly AuthSettings _settings;

        public SyncController(ITransactionsModule transactionsModule, AuthSettings settings)
        {
            _transactionsModule = transactionsModule;
            _settings = settings;
        }

        // GET api/sync/export
        [HttpGet("backup")]
        [ProducesResponseType(typeof(BackUpDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Export()
        {
            var backup = await _transactionsModule.ExecuteQueryAsync(new GetBackupQuery());
            return Ok(backup);
        }

        // POST api/sync/import
        [HttpPost("backup")]
        public async Task<IActionResult> Import([FromBody] BackUpDTO json)
        {
            await _transactionsModule.ExecuteCommandAsync(new ImportBackupCommand(json));
            return Ok();
        }

        // POST api/sync/sync
        [HttpPost("sync")]
        public async Task<IActionResult> Sync([FromBody] SyncRequest request)
        {
            //temporary no CQRS

            if (Request.Headers["Api-Key"] != _settings.ApiKey)
            {
                return Unauthorized();
            }

            var response = await _transactionsModule.ExecuteCommandAsync(new SyncCommand(request));
            return Ok(response);
        }
    }
}
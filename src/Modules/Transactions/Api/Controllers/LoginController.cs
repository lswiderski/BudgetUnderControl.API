using BudgetUnderControl.Modules.Transactions.Application.Commands.Login.Authenticate;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Login.CreateNewUser;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;
        private readonly AuthSettings settings;

        public LoginController(ITransactionsModule transactionsModule, AuthSettings settings)
        {
            _transactionsModule = transactionsModule;
            this.settings = settings;
        }


        [HttpPost("Mobile")]
        public async Task<IActionResult> LoginMobile([FromBody] AuthenticateCommand command)
        {
            if (Request.Headers["Api-Key"] != settings.ApiKey)
            {
                return Unauthorized();
            }

            var token = await _transactionsModule.ExecuteCommandAsync(command);

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login([FromBody] AuthenticateCommand command)
        {

            var token = await _transactionsModule.ExecuteCommandAsync(command);

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateNewUserCommand command)
        {
            var token = await _transactionsModule.ExecuteCommandAsync(command);

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

    }
}

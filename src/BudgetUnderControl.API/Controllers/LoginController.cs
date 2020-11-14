using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.CommonInfrastructure.Commands;
using BudgetUnderControl.CommonInfrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BudgetUnderControl.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache cache;
        private readonly GeneralSettings settings;

        public LoginController(ICommandDispatcher commandDispatcher,
            GeneralSettings settings,
            IMemoryCache cache) : base(commandDispatcher)
        {
            this.cache = cache;
            this.settings = settings;
        }

        [HttpPost("Mobile")]
        public async Task<IActionResult> LoginMobile([FromBody]MobileLoginCommand command)
        {
            if (Request.Headers["Api-Key"] != settings.ApiKey)
            {
                return Unauthorized();
            }

            command.TokenId = Guid.NewGuid();
            await DispatchAsync(command);
            var token = cache.Get<string>(command.TokenId);

            if(string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login([FromBody]MobileLoginCommand command)
        {
            command.TokenId = Guid.NewGuid();
            await DispatchAsync(command);
            var token = cache.Get<string>(command.TokenId);

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            command.TokenId = Guid.NewGuid();
            await DispatchAsync(command);
            var token = cache.Get<string>(command.TokenId);

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

       
    }
}
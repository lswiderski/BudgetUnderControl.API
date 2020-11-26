using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Common.Contracts.User;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.CommonInfrastructure.Commands;
using BudgetUnderControl.CommonInfrastructure.Commands.Login;
using BudgetUnderControl.CommonInfrastructure.Commands.User;
using BudgetUnderControl.CommonInfrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BudgetUnderControl.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly IMemoryCache cache;
        private readonly IUserIdentityContext userIdentityContext;
        private readonly IUserAdminService userAdminService;

        public UsersController(ICommandDispatcher commandDispatcher,
            IMemoryCache cache, 
            IUserIdentityContext userIdentityContext,
            IUserAdminService userAdminService) : base(commandDispatcher)
        {
            this.cache = cache;
            this.userIdentityContext = userIdentityContext;
            this.userAdminService = userAdminService;
        }

        [HttpPost("Logout")]
        [Authorize(Policy = UsersPolicy.AllUsers)]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }

        [HttpGet("IdentityContext")]
        [Authorize(Policy = UsersPolicy.AllUsers)]
        public async Task<ActionResult<IUserIdentityContext>> GetIdentityContext()
        {
            return Ok(userIdentityContext);
        }

        [HttpPost("Activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateUserCommand command)
        {
            command.UserId = this.userIdentityContext.ExternalId;
            var result = await DispatchWithResultAsync(command);
            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("ResetActivation")]
        public async Task<IActionResult> ResetActivationCode()
        {
            var command = new ResetActivationCodeCommand { UserId = this.userIdentityContext.ExternalId };
            await DispatchAsync(command);
            return Ok();
        }

        [HttpPost("{id}/ResetActivation")]
        [Authorize(Policy = UsersPolicy.Admins)]
        public async Task<IActionResult> ResetActivationCode(Guid id)
        {
            var command = new ResetActivationCodeCommand { UserId = id };
            await DispatchAsync(command);
            return Ok();
        }


        [HttpGet]
        [Authorize(Policy = UsersPolicy.Admins)]
        public async Task<ActionResult<IEnumerable<UserListItemDTO>>> Get()
        {
            var transactions = await this.userAdminService.GetUsersAsync();
            return Ok(transactions.ToList());
        }
        [HttpGet("{id}")]
        [Authorize(Policy = UsersPolicy.Admins)]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            var user = await this.userAdminService.GetUserAsync(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UsersPolicy.Admins)]
        public async Task<IActionResult> Put(Guid id, [FromBody] EditUser command)
        {
            await this.DispatchAsync(command);
            return NoContent();
        }

    }
}

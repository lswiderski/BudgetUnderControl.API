using BudgetUnderControl.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Users.Application.Contracts;
using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Modules.Users.Application.Commands.Users.GetUserIdentity;
using BudgetUnderControl.Modules.Users.Application.Commands.Users.ActivateUser;
using BudgetUnderControl.Modules.Users.Application.Commands.Users.ResetUserActivation;
using BudgetUnderControl.Modules.Users.Application.DTO;
using BudgetUnderControl.Modules.Users.Application.Commands.Users.GetUser;
using BudgetUnderControl.Modules.Users.Application.Commands.Users.GetUsers;
using BudgetUnderControl.Modules.Users.Application.Commands.Users.UpdateUser;
using BudgetUnderControl.Shared.Abstractions.Contexts;

namespace BudgetUnderControl.Modules.Users.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersModule _usersModule;

        public UsersController(IUsersModule usersModule)
        {
            _usersModule = usersModule;
        }

        [HttpPost("Logout")]
        [Authorize(Policy = UsersPolicy.AllUsers)]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }

        [HttpGet("IdentityContext")]
        [Authorize(Policy = UsersPolicy.AllUsers)]
        [ProducesResponseType(typeof(IUserIdentityContext), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IUserIdentityContext>> GetIdentityContext()
        {
            var identityContext = await _usersModule.ExecuteQueryAsync(new GetUserIdentityQuery());
            return Ok(identityContext);
        }

        [HttpPost("Activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateUserCommand command)
        {
 
            var isSuccess = await _usersModule.ExecuteCommandAsync(command);
            if (isSuccess)
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
            await _usersModule.ExecuteCommandAsync(new ResetUserActivationCommand());
            return Ok();
        }

        [HttpPost("{id}/ResetActivation")]
        [Authorize(Policy = UsersPolicy.Admins)]
        public async Task<IActionResult> ResetActivationCode(Guid id)
        {
            await _usersModule.ExecuteCommandAsync(new ResetUserActivationCommand(id));
            return Ok();
        }


        [HttpGet]
        [Authorize(Policy = UsersPolicy.Admins)]
        [ProducesResponseType(typeof(IEnumerable<UserListItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserListItemDTO>>> Get()
        {
            var users = await _usersModule.ExecuteQueryAsync(new GetUsersQuery()); 
            return Ok(users.ToList());
        }
        [HttpGet("{id}")]
        [Authorize(Policy = UsersPolicy.Admins)]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            var user = await _usersModule.ExecuteQueryAsync(new GetUserQuery(id));
            return Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UsersPolicy.Admins)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateUserCommand command)
        {
            command.UserId = id;
            await _usersModule.ExecuteCommandAsync(command);
            return NoContent();
        }

    }
}

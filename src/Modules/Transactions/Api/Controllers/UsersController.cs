using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Users.ActivateUser;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Users.GetUser;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Users.GetUserIdentity;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Users.GetUsers;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Users.ResetUserActivation;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Users.UpdateUser;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;

        public UsersController(ITransactionsModule transactionsModule)
        {
            _transactionsModule = transactionsModule;
        }

/*
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
            var identityContext = await _transactionsModule.ExecuteQueryAsync(new GetUserIdentityQuery());
            return Ok(identityContext);
        }

        [HttpPost("Activate")]
        public async Task<IActionResult> Activate([FromBody] ActivateUserCommand command)
        {
 
            var isSuccess = await _transactionsModule.ExecuteCommandAsync(command);
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
            await _transactionsModule.ExecuteCommandAsync(new ResetUserActivationCommand());
            return Ok();
        }

        [HttpPost("{id}/ResetActivation")]
        [Authorize(Policy = UsersPolicy.Admins)]
        public async Task<IActionResult> ResetActivationCode(Guid id)
        {
            await _transactionsModule.ExecuteCommandAsync(new ResetUserActivationCommand(id));
            return Ok();
        }


        [HttpGet]
        [Authorize(Policy = UsersPolicy.Admins)]
        [ProducesResponseType(typeof(IEnumerable<UserListItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserListItemDTO>>> Get()
        {
            var users = await _transactionsModule.ExecuteQueryAsync(new GetUsersQuery()); 
            return Ok(users.ToList());
        }
        [HttpGet("{id}")]
        [Authorize(Policy = UsersPolicy.Admins)]
        [ProducesResponseType(typeof(UserDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id)
        {
            var user = await _transactionsModule.ExecuteQueryAsync(new GetUserQuery(id));
            return Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = UsersPolicy.Admins)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateUserCommand command)
        {
            await _transactionsModule.ExecuteCommandAsync(command);
            return NoContent();
        }
*/
    }
}

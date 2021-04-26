using BudgetUnderControl.Modules.Users.Application.Commands.Login.Authenticate;
using BudgetUnderControl.Modules.Users.Application.Commands.Login.AuthenticateAdmin;
using BudgetUnderControl.Modules.Users.Application.Commands.Login.CreateNewUser;
using BudgetUnderControl.Modules.Users.Application.Contracts;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersModule _usersModule;
        private readonly AuthSettings settings;

        public LoginController(IUsersModule usersModule, AuthSettings settings)
        {
            _usersModule = usersModule;
            this.settings = settings;
        }


        [HttpPost("Mobile")]
        public async Task<IActionResult> LoginMobile([FromBody] AuthenticateCommand command)
        {
            if (Request.Headers["Api-Key"] != settings.ApiKey)
            {
                return Unauthorized();
            }

            var token = await _usersModule.ExecuteCommandAsync(command);

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login([FromBody] AuthenticateCommand command)
        {

            var token = await _usersModule.ExecuteCommandAsync(command);

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost("AdminAuthenticate")]
        public async Task<IActionResult> AdminLogin([FromBody] AuthenticateAdminCommand command)
        {

            var token = await _usersModule.ExecuteCommandAsync(command);

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(new { AccessToken = token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] CreateNewUserCommand command)
        {
            var token = await _usersModule.ExecuteCommandAsync(command);

            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                return Unauthorized();
            }

            return Ok(token);
        }

    }
}

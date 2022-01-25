using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Login.Authenticate
{
    internal class AuthenticateCommandHandler : ICommandHandler<AuthenticateCommand, string>
    {
        private readonly IUserService userService;

        internal AuthenticateCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<string> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var userId = await this.userService.ValidateLoginAsync(request.Username, request.Password);
            if(userId is {})
            {
                var token = await this.userService.CreateAccessTokenAsync(userId.Value);
                return token;
            }
            else
            {
                return null;
            }
        }
}
}

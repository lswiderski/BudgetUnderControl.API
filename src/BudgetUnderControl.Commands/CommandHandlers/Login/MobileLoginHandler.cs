using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure.Commands.Login
{
    public class MobileLoginHandler : ICommandHandler<MobileLoginCommand>
    {
        private readonly IUserService loginService;
        public MobileLoginHandler(IUserService loginService)
        {
            this.loginService = loginService;
        }

        public async Task HandleAsync(MobileLoginCommand command)
        {
            await this.loginService.ValidateLoginAsync(command);
        }
    }
}

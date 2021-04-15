using BudgetUnderControl.Modules.Users.Domain.Entities;

namespace BudgetUnderControl.Modules.Users.Application.Services
{
    public interface IJwtHandlerService
    {
        string CreateToken(User user);
    }
}

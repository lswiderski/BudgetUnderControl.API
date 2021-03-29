using BudgetUnderControl.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface IJwtHandlerService
    {
        string CreateToken(User user);
    }
}

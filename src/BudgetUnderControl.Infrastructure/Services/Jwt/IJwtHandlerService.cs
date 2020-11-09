using BudgetUnderControl.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Infrastructure
{
    public interface IJwtHandlerService
    {
        string CreateToken(User user);
    }
}

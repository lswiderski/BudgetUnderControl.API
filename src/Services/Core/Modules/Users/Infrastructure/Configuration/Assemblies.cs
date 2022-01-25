using BudgetUnderControl.Modules.Users.Application.Commands.Users.ActivateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(ActivateUserCommand).Assembly;

        public static readonly Assembly Infrastructure = typeof(UsersModuleExecutor).Assembly;
    }
}

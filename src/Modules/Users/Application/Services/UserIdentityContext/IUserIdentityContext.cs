using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Modules.Users.Application.Services
{
    public interface IUserIdentityContext
    {
        Guid UserId { get; }

        UserRole Role { get; }

        public bool IsAdmin { get; }

        string RoleName { get; }

        public bool IsActivated { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }
    }
}

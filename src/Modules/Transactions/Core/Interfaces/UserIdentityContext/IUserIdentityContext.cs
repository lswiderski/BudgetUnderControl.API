using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface IUserIdentityContext
    {
        int UserId { get; }

        Guid ExternalId { get; }

        UserRole Role { get; }

        public bool IsAdmin { get; }

        string RoleName { get; }

        public bool IsActivated { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }
    }
}

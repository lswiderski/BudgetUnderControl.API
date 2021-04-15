using System;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Shared.Abstractions.Enums;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Services
{
    public class UserIdentityContext : IUserIdentityContext
    {
        public Guid UserId { get; set; }

        public UserRole Role
        {
            get
            {
                return this.RoleName.GetEnumValue<UserRole>();
            }
        }

        public bool IsAdmin
        {
            get
            {
                return this.Role == UserRole.Admin;
            }
        }

        public string RoleName { get; set; }

        public bool IsActivated { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

    }
}

using System;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Common.Extensions;
using BudgetUnderControl.Modules.Transactions.Application.Services;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class UserIdentityContext : IUserIdentityContext
    {
        public int UserId { get; set; }

        public Guid ExternalId { get; set; }

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

using System;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Common.Extensions;
using BudgetUnderControl.Modules.Transactions.Application.Services;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class UserIdentityContext
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

        public DateTime? ModifiedOn { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Username { get; set; }

        public bool IsDeleted { get; set; }


    }
}

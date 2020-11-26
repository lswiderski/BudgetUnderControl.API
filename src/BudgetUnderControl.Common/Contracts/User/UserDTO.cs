using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Contracts.User
{
    public class UserDTO
    {
        public int Id { get; protected set; }
        public string Username { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; set; }
        public UserRole Role { get; protected set; }
        public string RoleName 
        {
            get
            {
                return this.Role.GetStringValue();
            }
        }
        public string Email { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? ModifiedOn { get; protected set; }
        public Guid ExternalId { get; protected set; }
        public bool IsDeleted { get; protected set; }
        public bool IsActivated { get; set; }
        public DateTime? ActivatedOn { get; protected set; }
        public string ActivationCode { get; set; }
    }
}

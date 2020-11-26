using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands.User
{
    public class EditUser : ICommand
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsActivated { get; set; }
    }
}

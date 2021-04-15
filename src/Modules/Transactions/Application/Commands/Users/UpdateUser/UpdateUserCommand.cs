using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Users.UpdateUser
{
    public class UpdateUserCommand : CommandBase
    {
        public UpdateUserCommand(string username, string firstname, string lastname, UserRole role, string email, Guid userId, bool isActivated)
        {
            Username = username;
            FirstName = firstname;
            LastName = lastname;
            Role = role;
            Email = email;
            ExternalId = userId;
            IsActivated = isActivated;
        }

        public UpdateUserCommand()
        {

        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string Email { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsActivated { get; set; }
    }
}

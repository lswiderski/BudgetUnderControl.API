using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using System;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Users.UpdateUser
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
            UserId = userId;
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
        public Guid UserId { get; set; }
        public bool IsActivated { get; set; }
    }
}

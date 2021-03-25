using BudgetUnderControl.Domain.Repositiories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Users.UpdateUser
{
    internal class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private readonly IUserRepository userRepository;
        public UpdateUserCommandValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(t => t.Username).NotEmpty().Length(1, 50)
                .CustomAsync(async (id, context, cancel) =>
                {



                })
                .MustAsync(IsNameUniqueAsync).WithMessage("Username must be unique");

            RuleFor(t => t.Email).NotNull().NotEmpty().Length(1, 150)
                .EmailAddress().MustAsync(IsEmailUniqueAsync).WithMessage("User with that email already exist");
        }

        private async Task<bool> IsNameUniqueAsync(UpdateUserCommand command, string name, CancellationToken cancel)
        {
            var user = await userRepository.GetAsync(name);

            if (user != null && user.ExternalId != command.ExternalId)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> IsEmailUniqueAsync(UpdateUserCommand command, string email, CancellationToken cancel)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            var user = await userRepository.GetByEmailAsync(email.ToLower());

            if (user != null && user.ExternalId != command.ExternalId)
            {
                return false;
            }
            return true;
        }
    }
}


using BudgetUnderControl.Domain.Repositiories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Login.CreateNewUser
{
    internal class CreateNewUserCommandValidator : AbstractValidator<CreateNewUserCommand>
    {
        public CreateNewUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(t => t.Password).NotEmpty();

            RuleFor(t => t.Username).NotEmpty().Length(1, 50).CustomAsync(async (name, context, cancel) =>
            {
                var user = await userRepository.GetAsync(name);

                if (user != null)
                {
                    context.AddFailure("Username must be unique");
                }

            });

            RuleFor(t => t.Email).NotNull().NotEmpty().Length(1, 150).EmailAddress().CustomAsync(async (email, context, cancel) =>
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var user = await userRepository.GetByEmailAsync(email.ToLower());

                    if (user != null)
                    {
                        context.AddFailure("User with that email already exist");
                    }
                }
            });
        }
    }
}

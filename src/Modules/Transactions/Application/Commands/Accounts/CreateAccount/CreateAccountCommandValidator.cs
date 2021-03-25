using BudgetUnderControl.CommonInfrastructure;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.CreateAccount
{
    internal class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator(ICurrencyService currencyService, IAccountGroupService accountGroupService, IAccountService accountService)
        {
            RuleFor(t => t.AccountGroupId).NotEmpty();
            RuleFor(t => t.Name).NotEmpty().Length(1, 100);

            RuleFor(t => t.CurrencyId).NotEmpty().CustomAsync(async (id, context, cancel) =>
            {
                var isValid = await currencyService.IsValidAsync(id);
                if (!isValid)
                {
                    context.AddFailure("That Currency is not Exist");
                }
            });
            RuleFor(t => t.ParentAccountId).CustomAsync(async (id, context, cancel) =>
            {
                if (id.HasValue)
                {
                    var isValid = await accountService.IsValidAsync(id.Value);
                    if (!isValid)
                    {
                        context.AddFailure("This user do not own that Account");
                    }
                }
            });

            RuleFor(t => t.AccountGroupId).NotEmpty().CustomAsync(async (id, context, cancel) =>
            {
                var isValid = await accountGroupService.IsValidAsync(id);
                if (!isValid)
                {
                    context.AddFailure("This user do not own that Account Group");
                }

            });
        }
    }
}

﻿using BudgetUnderControl.CommonInfrastructure.Commands;
using BudgetUnderControl.Infrastructure.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using BudgetUnderControl.CommonInfrastructure;

namespace BudgetUnderControl.ApiInfrastructure.Commands.Validators
{
    public class EditAccountValidator : AbstractValidator<EditAccount>
    {
        public EditAccountValidator(ICurrencyService currencyService, IAccountGroupService accountGroupService, IAccountService accountService)
        {
            RuleFor(t => t.Id).NotEmpty();
            RuleFor(t => t.ExternalId).NotEmpty();
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

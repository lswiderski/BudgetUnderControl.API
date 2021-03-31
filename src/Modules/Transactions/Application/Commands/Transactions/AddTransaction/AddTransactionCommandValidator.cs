using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.AddTransaction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Transactions.AddTransaction
{
    internal class AddTransactionCommandValidator : AbstractValidator<AddTransactionCommand>
    {
        public AddTransactionCommandValidator(ICategoryService categoryService, IAccountService accountService)
        {
            RuleFor(t => t.Amount).NotEmpty();
            RuleFor(t => t.Date).NotEmpty();
            RuleFor(t => t.Name).NotEmpty().Length(1, 100);
            RuleFor(t => t.Comment).Length(0, 1000);

            RuleFor(t => t.TransferAmount).NotEmpty().When(t => t.Type == ExtendedTransactionType.Transfer);
            RuleFor(t => t.TransferDate).NotEmpty().When(t => t.Type == ExtendedTransactionType.Transfer);
            RuleFor(t => t.Rate).NotEmpty().When(t => t.Type == ExtendedTransactionType.Transfer);

            RuleFor(t => t.CategoryId).NotNull().CustomAsync(async (id, context, cancel) =>
            {
                if (id.HasValue)
                {
                    var isValid = await categoryService.IsValidAsync(id.Value);

                    if (!isValid)
                    {
                        context.AddFailure("This user do not own that Category");
                    }
                }


            });

            RuleFor(t => t.AccountId).NotEmpty().CustomAsync(async (id, context, cancel) =>
            {
                var isValid = await accountService.IsValidAsync(id);

                if (!isValid)
                {
                    context.AddFailure("This user do not own that Account");
                }
            });

            RuleFor(t => t.TransferAccountId).NotEmpty().When(t => t.Type == ExtendedTransactionType.Transfer).CustomAsync(async (id, context, cancel) =>
            {
                if ((context.InstanceToValidate as AddTransactionCommand).Type == ExtendedTransactionType.Transfer)
                {
                    var isValid = await accountService.IsValidAsync(id.Value);
                    if (!isValid)
                    {
                        context.AddFailure("This user do not own that Account");
                    }
                }
            });
        }

    }
}

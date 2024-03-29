﻿using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Processing
{
    internal class ValidationCommandHandlerWithResultDecorator<T, TResult> : IRequestHandler<T, TResult>
        where T : ICommand<TResult>
    {
        private readonly IList<IValidator<T>> _validators;
        private readonly IRequestHandler<T, TResult> _decorated;

        public ValidationCommandHandlerWithResultDecorator(
            IList<IValidator<T>> validators,
            IRequestHandler<T, TResult> decorated)
        {
            this._validators = validators;
            _decorated = decorated;
        }

        public Task<TResult> Handle(T command, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(v => v.Validate(command))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                //throw new InvalidCommandException(errors.Select(x => x.ErrorMessage).ToList());
            }

            return _decorated.Handle(command, cancellationToken);
        }
    }
}
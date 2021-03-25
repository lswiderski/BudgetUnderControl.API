using Autofac;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Processing
{
    internal class ProcessingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGenericDecorator(
                typeof(ValidationCommandHandlerDecorator<>),
                typeof(ICommandHandler<>));

            builder.RegisterGenericDecorator(
                typeof(ValidationCommandHandlerWithResultDecorator<,>),
                typeof(ICommandHandler<,>));

        }
    }
}

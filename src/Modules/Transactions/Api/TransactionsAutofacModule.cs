using Autofac;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using BudgetUnderControl.Modules.Transactions.Infrastructure;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Mediation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Api
{
    public class TransactionsAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionsModuleExecutor>()
                .As<ITransactionsModule>()
                .InstancePerLifetimeScope();


            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new Infrastructure.Configuration.TransactionsModule());
        }
    }
}

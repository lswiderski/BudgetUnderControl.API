using Autofac;
using BudgetUnderControl.Domain;
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
        private readonly IContextConfig contextConfig;

        public TransactionsAutofacModule(IContextConfig contextConfig)
        {
            this.contextConfig = contextConfig;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionsModuleExecutor>()
                .As<ITransactionsModule>()
                .InstancePerLifetimeScope();


           
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new Infrastructure.Configuration.TransactionsModule(contextConfig));
        }
    }
}

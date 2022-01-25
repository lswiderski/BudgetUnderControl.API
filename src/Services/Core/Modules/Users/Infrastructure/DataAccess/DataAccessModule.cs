using Autofac;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Infrastructure.DataAccess
{
    internal class DataAccessModule : Autofac.Module
    {
        private readonly IContextConfig contextConfig;

        internal DataAccessModule(IContextConfig contextConfig)
        {
            this.contextConfig = contextConfig;
        }

        protected override void Load(ContainerBuilder builder)
        {
            /*
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();
            */
            /*
           var infrastructureAssembly = typeof(TransactionsContext).Assembly;

           builder.RegisterAssemblyTypes(infrastructureAssembly)
               .Where(type => type.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope()
               .FindConstructorsWith(new AllConstructorFinder());
           */
        }
    }
}

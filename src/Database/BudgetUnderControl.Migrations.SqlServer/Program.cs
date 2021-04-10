using Autofac;
using BudgetUnderControl.Common;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BudgetUnderControl.Migrations.SqlServer
{
    class Program : IDesignTimeDbContextFactory<TransactionsContext>
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            using (TransactionsContext context = p.CreateDbContext(null))
            {

            }
        }

        public TransactionsContext CreateDbContext(string[] args)
        {

            var contextConfig = new ContextConfig() { DbName = "dbBUC", Application = ApplicationType.SqlServerMigrations, ConnectionString= "Data Source=.;Initial Catalog=dbBUC-dev;User ID=buc;Password=Qwerty!1" };
            var connectionString = contextConfig.ConnectionString;


            DbContextOptionsBuilder<TransactionsContext> optionsBuilder = new DbContextOptionsBuilder<TransactionsContext>()
                .UseSqlServer(connectionString);

             return new TransactionsContext(optionsBuilder.Options, contextConfig);
        }
    }


}

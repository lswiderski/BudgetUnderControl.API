﻿using Autofac;
using BudgetUnderControl.ApiInfrastructure.Repositories;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Infrastructure;
using BudgetUnderControl.Infrastructure.Repositories;
using BudgetUnderControl.Infrastructure.Services;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Mediation;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration.Processing;
using BudgetUnderControl.Modules.Transactions.Infrastructure.DataAccess;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Services;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration
{
    public class TransactionsAutofacModule : Autofac.Module
    {
        private readonly IConfiguration configuration;
        private readonly GeneralSettings settings;
        public TransactionsAutofacModule(IConfiguration configuration, GeneralSettings settings)
        {
            this.configuration = configuration;
            this.settings = settings;
        }


        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TransactionsModuleExecutor>()
              .As<ITransactionsModule>()
              .InstancePerLifetimeScope();

            var contextConfig = new ContextConfig() { DbName = settings.BUC_DB_Name, Application = settings.ApplicationType, ConnectionString = settings.ConnectionString };


       
          

           

            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyService>().As<ICurrencyService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<AccountGroupService>().As<IAccountGroupService>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();
            builder.RegisterType<ReportService>().As<IReportService>().InstancePerLifetimeScope();
            builder.RegisterType<ExpensesReportService>().As<IExpensesReportService>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyRepository>().As<ICurrencyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AccountRepository>().As<IAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AccountGroupRepository>().As<IAccountGroupRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TagRepository>().As<ITagRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SyncService>().As<ISyncService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<TagService>().As<ITagService>().InstancePerLifetimeScope();
            builder.RegisterType<TestDataSeeder>().As<ITestDataSeeder>().InstancePerLifetimeScope();
            builder.RegisterType<SynchronizationRepository>().As<ISynchronizationRepository>().InstancePerLifetimeScope();
            builder.RegisterType<Synchroniser>().As<ISynchroniser>().InstancePerLifetimeScope();
            builder.RegisterType<SyncRequestBuilder>().As<ISyncRequestBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserAdminService>().As<IUserAdminService>().InstancePerLifetimeScope();
            builder.RegisterType<Encrypter>().As<IEncrypter>().InstancePerLifetimeScope();
            builder.RegisterType<JwtHandlerService>().As<IJwtHandlerService>().InstancePerLifetimeScope();
            builder.RegisterType<FileService>().As<IFileService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailBuilder>().As<IEmailBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope();
            builder.RegisterType<TokenRepository>().As<ITokenRepository>().InstancePerLifetimeScope();

            builder.Register<Func<IUserIdentityContext>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return () =>
                {
                    var service = context.Resolve<IUserService>();
                    var httpAccessor = context.Resolve<IHttpContextAccessor>();
                    var identity = service.CreateUserIdentityContext(httpAccessor.HttpContext.User.Identity.Name);
                    return identity;
                };
            });

            builder.Register<IUserIdentityContext>(c =>
            {
                return c.Resolve<Func<IUserIdentityContext>>()();
            });

            builder.RegisterModule(new DataAccessModule(contextConfig));
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new ProcessingModule());

        }

    }
}
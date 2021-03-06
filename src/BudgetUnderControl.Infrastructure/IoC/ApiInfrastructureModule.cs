﻿using Autofac;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.CommonInfrastructure.Commands;
using BudgetUnderControl.Infrastructure.Repositories;
using BudgetUnderControl.Infrastructure.Services;
using BudgetUnderControl.Infrastructure.Services.UserService;
using BudgetUnderControl.Infrastructure;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.ApiInfrastructure.Services;

namespace BudgetUnderControl.Infrastructure.IoC
{
    public class ApiInfrastructureModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ApiInfrastructureModule)
            .GetTypeInfo()
            .Assembly;

            var commonInfrastructureAssembly = typeof(AddAccount)
            .GetTypeInfo()
            .Assembly;


            builder.RegisterAssemblyTypes(commonInfrastructureAssembly)
                    .AsClosedTypesOf(typeof(ICommandHandler<>))
                    .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                   .AsClosedTypesOf(typeof(IValidator<>))
                   .InstancePerLifetimeScope();

            builder.RegisterType<BaseModel>().As<IBaseModel>().InstancePerLifetimeScope();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<CurrencyService>().As<ICurrencyService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<AccountGroupService>().As<IAccountGroupService>().InstancePerLifetimeScope();
            builder.RegisterType<TransactionService>().As<ITransactionService>().InstancePerLifetimeScope();
            builder.RegisterType<ReportService>().As< IReportService>().InstancePerLifetimeScope();
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
            builder.RegisterType<LoginService>().As<ILoginService>().InstancePerLifetimeScope();
            builder.RegisterType<Encrypter>().As<IEncrypter>().InstancePerLifetimeScope();
            builder.RegisterType<JwtHandlerService>().As<IJwtHandlerService>().InstancePerLifetimeScope();
            builder.RegisterType<FileService>().As<IFileService>().InstancePerLifetimeScope();



            builder.Register<Func<IUserIdentityContext>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return () =>
                {
                    var service = context.Resolve<IUserService>();
                    var identity = service.CreateUserIdentityContext();
                    return identity;
                };
            });

            builder.Register<IUserIdentityContext>(c =>
            {
                return c.Resolve<Func<IUserIdentityContext>>()();
            });
        }
    }
}

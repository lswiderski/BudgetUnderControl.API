using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using BudgetUnderControl.API.Framework;
using BudgetUnderControl.API.IoC;
using BudgetUnderControl.Common;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Domain;

using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BudgetUnderControl.API.Extensions;
using System.Text;
using Microsoft.Extensions.Hosting;
using BudgetUnderControl.Common.Extensions;
using AutoMapper;
using BudgetUnderControl.ApiInfrastructure.Profiles.User;
using BudgetUnderControl.Modules.Transactions.Api;
using Microsoft.AspNetCore.Http;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Configuration;
using System.IO;
using BudgetUnderControl.Shared.Abstractions.Modules;
using System.Reflection;

namespace BudgetUnderControl.API
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment environment { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        public GeneralSettings Settings { get; private set; }

        private readonly IList<Assembly> _assemblies;
        private readonly IList<IModule> _modules;

        public Startup(IWebHostEnvironment env)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddJsonFile($"secrets.json", optional: true)
            .AddEnvironmentVariables();
            Configuration = configuration.Build();
            environment = env;

            Settings = Configuration.GetSettings<GeneralSettings>().InjectEnvVariables();
          
            if (string.IsNullOrWhiteSpace(environment.WebRootPath))
            {
                environment.WebRootPath =Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
            Settings.FileRootPath = environment.WebRootPath;

            _assemblies = ModuleLoader.LoadAssemblies(Configuration);
            _modules = ModuleLoader.LoadModules(_assemblies);

            Console.WriteLine("Connection string: " + Settings.ConnectionString);
            Console.WriteLine("Application Type:  " + Settings.ApplicationType.ToString());
            Console.WriteLine("DB Name:  " + Settings.BUC_DB_Name);
            Console.WriteLine($"Modules: {string.Join(", ", _modules.Select(x => x.Name))}");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutofac();
            services.AddMemoryCache();
            services.AddCors();
            services.AddAutoMapper(typeof(UserProfile));
            services.AddOptions();
            //services.AddAuthorization();
            services.AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.WriteIndented = true;
                });
            services.AddHttpContextAccessor();
            services.AddBUCAuthentication(Settings.SecretKey);
            services.AddBUCAuthorization();

            foreach (var module in _modules)
            {
                module.Register(services);
            }
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add things to the Autofac ContainerBuilder.
            builder.RegisterInstance(Settings)
              .SingleInstance();

            builder.RegisterModule(new ApiModule());

            foreach (var module in _modules)
            {
                module.ConfigureContainer(builder, Settings);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCustomExceptionHandler();
            app.UseCors(options =>
                    options
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true)
                        .AllowCredentials()
                        );

            app.UseAuthentication();
            app.UseAuthorization();

            foreach (var module in _modules)
            {
                module.Use(app);
            }
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Budget Under Control API"));
            });

            _assemblies.Clear();
            _modules.Clear();
        }
    }
}

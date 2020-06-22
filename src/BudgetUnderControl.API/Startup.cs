﻿using System;
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
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Infrastructure.IoC;
using BudgetUnderControl.Infrastructure.Repositories;
using BudgetUnderControl.Infrastructure.Services;
using BudgetUnderControl.Infrastructure.Services.UserService;
using BudgetUnderControl.Infrastructure;
using Microsoft.AspNetCore.Cors;
using CommonServiceLocator;
using FluentValidation;
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
using BudgetUnderControl.CommonInfrastructure.Settings;
using Microsoft.Extensions.Hosting;

namespace BudgetUnderControl.API
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddJsonFile($"secrets.json", optional: true)
            .AddEnvironmentVariables();
            Configuration = configuration.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddJsonOptions(x => {
                    x.JsonSerializerOptions.WriteIndented = true;
                    })
                .AddFluentValidation();

            services.AddDbContext<Context>(ServiceLifetime.Transient);
            services.AddTransient<IContextFacade, ContextFacade>();
            services.AddMemoryCache();
            services.AddCors();
            services.AddControllers();

            var settings = Configuration.GetSettings<GeneralSettings>();

            var key = Encoding.ASCII.GetBytes(settings.SecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });

            // Initialize Autofac builder
            var builder = new ContainerBuilder();          

            builder.RegisterModule<ApiInfrastructureModule>();
            builder.RegisterModule(new ApiModule(Configuration));
            builder.Populate(services);
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IContextConfig contextConfig)//, ITestDataSeeder testDataSeeder)
        {
            if(contextConfig.Application == ApplicationType.Test)
            {
                //testDataSeeder.SeedAsync().Wait();
            }

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
            app.UseAuthentication();
            app.UseCors(options =>
                    options
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true)
                        .AllowCredentials()
                        );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}

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

namespace BudgetUnderControl.API
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        private IWebHostEnvironment environment { get; }

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
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TransactionsContext>(ServiceLifetime.Transient);
           
            services.AddMemoryCache();
            services.AddCors();
            services.AddAutoMapper(typeof(UserProfile));
            services.AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.WriteIndented = true;
                });
            /*
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<ApiInfrastructureModule>();
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });*/
            services.AddHttpContextAccessor();

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

            services.AddAuthorization(options =>
            {
                options.AddPolicy(UsersPolicy.AllUsers, policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(UserRole.User.GetStringValue(), UserRole.LimitedUser.GetStringValue(), UserRole.PremiumUser.GetStringValue(), UserRole.Admin.GetStringValue());
                });

                options.AddPolicy(UsersPolicy.Admins, policy =>
                {
                    policy.AddAuthenticationSchemes("Bearer");
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(UserRole.Admin.GetStringValue());
                });
            });

            // Initialize Autofac builder
            var builder = new ContainerBuilder();          

            builder.RegisterModule(new ApiModule(Configuration, environment));
            
            builder.Populate(services);
            ApplicationContainer = builder.Build();
            services.AddTransactionsModule(ApplicationContainer);
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
            app.UseTransactionsModule();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", context => context.Response.WriteAsync("Budget Under Control API"));
            });
        }
    }
}

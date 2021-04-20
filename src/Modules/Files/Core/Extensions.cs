using Autofac.Extensions.DependencyInjection;
using BudgetUnderControl.Modules.Files.Core.Configuration;
using BudgetUnderControl.Modules.Files.Core.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetUnderControl.Modules.Files.Core
{
    public  static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, string connectionString)
        {
            services
            .AddDbContext<FilesDbContext>(x => x.UseSqlServer(connectionString));
            return services;
        }

        public static IApplicationBuilder UseCore(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetAutofacRoot();
            FilesCompositionRoot.SetScope(scope);
            return app;
        }
    }
}
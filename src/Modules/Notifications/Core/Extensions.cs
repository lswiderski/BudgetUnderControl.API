using Autofac.Extensions.DependencyInjection;
using BudgetUnderControl.Modules.Notifications.Core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetUnderControl.Modules.Notifications.Core
{
    public  static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseCore(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.GetAutofacRoot();
            NotificationsCompositionRoot.SetScope(scope);
            return app;
        }
    }
}
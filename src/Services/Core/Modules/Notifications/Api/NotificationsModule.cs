using System.Runtime.CompilerServices;
using Autofac;
using BudgetUnderControl.Modules.Notifications.Core;
using BudgetUnderControl.Modules.Notifications.Core.Commands.ActivateUserNotification;
using BudgetUnderControl.Shared.Abstractions.Modules;
using BudgetUnderControl.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("BudgetUnderControl.Core")]
namespace BudgetUnderControl.Modules.Notifications.Api
{
    internal class NotificationsModule : IModule
    {
        public const string BasePath = "notifications-module";
        public string Name { get; } = "Notifications";
        public string Path => BasePath;

        public void ConfigureContainer(ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterModule(new NotificationsAutofacModule(configuration));
        }

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseCore();
            
            app.UseModuleRequests()
                .Subscribe<CreateActivateUserNotificationCommand, object>("notification/createActivateUserNotification",
                    async (command, sp) =>
                    {
                        await sp.GetRequiredService<INotificationsModule>().ExecuteCommandAsync(command);
                        return null;
                    });
        }
    }
}

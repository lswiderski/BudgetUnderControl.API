using BudgetUnderControl.Shared.Abstractions.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using System.Threading.Tasks;

namespace BudgetUnderControl.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        internal static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
        {
            var moduleInfoProvider = new ModuleInfoProvider();
            var moduleInfo =
                modules?.Select(x => new ModuleInfo(x.Name, x.Path,  Enumerable.Empty<string>())) ??
                Enumerable.Empty<ModuleInfo>();
            moduleInfoProvider.Modules.AddRange(moduleInfo);
            services.AddSingleton(moduleInfoProvider);

            return services;
        }

        internal static void MapModuleInfo(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("modules", context =>
            {
                var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
                return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
            });
        }

        internal static IHostBuilder ConfigureModules(this IHostBuilder builder)
            => builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
            });

        internal static IServiceCollection AddModuleRequests(this IServiceCollection services,
            IList<Assembly> assemblies)
        {
            services.AddModuleRegistry(assemblies);
            services.AddSingleton<IModuleClient, ModuleClient>();
            services.AddSingleton<IModuleSerializer, JsonModuleSerializer>();
            services.AddSingleton<IModuleSubscriber, ModuleSubscriber>();

            return services;
        }

        public static IModuleSubscriber UseModuleRequests(this IApplicationBuilder app)
            => app.ApplicationServices.GetRequiredService<IModuleSubscriber>();

        private static void AddModuleRegistry(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var registry = new ModuleRegistry();
            var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();


            services.AddSingleton<IModuleRegistry>(sp =>
            {
                return registry;
            });

              var commandTypes = types
                  .Where(t => t.IsClass && typeof(ICommand).IsAssignableFrom(t))
                  .ToArray();

             /* var eventTypes = types
                  .Where(x => x.IsClass && typeof(IEvent).IsAssignableFrom(x))
                  .ToArray();
             */

              services.AddSingleton<IModuleRegistry>(sp =>
              {
                  //var commandDispatcher = sp.GetRequiredService<ICommandDispatcher>();
                //  var commandDispatcherType = commandDispatcher.GetType();

                //  var eventDispatcher = sp.GetRequiredService<IEventDispatcher>();
                //  var eventDispatcherType = eventDispatcher.GetType();

               /*   foreach (var type in commandTypes)
                  {
                      registry.AddBroadcastAction(type, @event =>
                          (Task)commandDispatcherType.GetMethod(nameof(commandDispatcher.ExecuteCommandAsync))
                              ?.MakeGenericMethod(type)
                              .Invoke(commandDispatcher, new[] { @event }));
                  }
               */
               /*   foreach (var type in eventTypes)
                  {
                      registry.AddBroadcastAction(type, @event =>
                          (Task)eventDispatcherType.GetMethod(nameof(eventDispatcher.PublishAsync))
                              ?.MakeGenericMethod(type)
                              .Invoke(eventDispatcher, new[] { @event }));
                  }
               */
                  return registry;
              });
              
        }
    }
}

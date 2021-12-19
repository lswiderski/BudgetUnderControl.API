using BudgetUnderControl.Shared.Abstractions.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetUnderControl.Shared.Infrastructure.Messaging
{
    internal static class Extensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            services.AddSingleton<IMessageChannel, MessageChannel>();
            services.AddSingleton<IAsynchronousDispatcher, AsynchronousDispatcher>();
            services.AddHostedService<AsynchronousDispatcherJob>();

            return services;
        }
    }
}
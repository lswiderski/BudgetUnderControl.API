using Autofac;

namespace BudgetUnderControl.Modules.Notifications.Core.Configuration
{
    internal static class NotificationsCompositionRoot
    {
        private static IContainer _container;
        private static ILifetimeScope _scope;

        internal static void SetContainer(IContainer container)
        {
            _container = container;
        }

        internal static void SetScope(ILifetimeScope scope)
        {
            _scope = scope;
        }

        internal static ILifetimeScope BeginLifetimeScope()
        {
            return _scope.BeginLifetimeScope();
        }
    }
}
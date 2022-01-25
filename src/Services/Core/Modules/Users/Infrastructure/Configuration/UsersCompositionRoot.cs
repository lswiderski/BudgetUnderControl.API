using Autofac;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Configuration
{
    internal static class UsersCompositionRoot
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

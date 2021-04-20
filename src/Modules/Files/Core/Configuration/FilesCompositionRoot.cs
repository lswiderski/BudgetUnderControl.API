using Autofac;
using IContainer = System.ComponentModel.IContainer;

namespace BudgetUnderControl.Modules.Files.Core.Configuration
{
    internal static class FilesCompositionRoot
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
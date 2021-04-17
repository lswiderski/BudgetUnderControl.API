using System.Reflection;

namespace BudgetUnderControl.Modules.Notifications.Core.Configuration
{
    internal static class Assemblies
    {

        public static readonly Assembly Core = typeof(NotificationsModuleExecutor).Assembly;
    }
}
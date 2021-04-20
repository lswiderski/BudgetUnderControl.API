using System.Reflection;

namespace BudgetUnderControl.Modules.Files.Core.Configuration
{
    internal static class Assemblies
    {

        public static readonly Assembly Core = typeof(FilesModuleExecutor).Assembly;
    }
}
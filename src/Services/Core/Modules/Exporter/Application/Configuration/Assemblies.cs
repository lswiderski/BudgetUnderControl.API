using System.Reflection;

namespace BudgetUnderControl.Modules.Exporter.Application.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(ExporterModuleExecutor).Assembly;
    }
}
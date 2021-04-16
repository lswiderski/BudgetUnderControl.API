using System.Collections.Generic;

namespace BudgetUnderControl.Shared.Infrastructure.Modules
{
    internal record ModuleInfo(string Name, string Path, IEnumerable<string> Policies);
}
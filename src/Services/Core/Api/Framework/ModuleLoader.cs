using BudgetUnderControl.Shared.Abstractions.Modules;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BudgetUnderControl.API.Framework
{
    internal static class ModuleLoader
    {
        public static IList<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            const string modulePart = "BudgetUnderControl.Modules.";
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location).ToArray();
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            var disabledModules = new List<string>();
            var notModuleFiles = new List<string>();
            foreach (var file in files)
            {
                if (!file.Contains(modulePart))
                {
                    notModuleFiles.Add(file);
                    continue;
                }

                var moduleName = file.Split(modulePart)[1].Split(".")[0].ToLowerInvariant();
               /* var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
                if (!enabled)
                {
                    disabledModules.Add(file);
                }
                */
            }

            foreach (var notModuleFile in notModuleFiles)
            {
                files.Remove(notModuleFile);
            }

            foreach (var disabledModule in disabledModules)
            {
                files.Remove(disabledModule);
            }

            files.ForEach(x => assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(x))));

            return assemblies;
        }

        public static IList<IModule> LoadModules(IEnumerable<Assembly> assemblies)
           => assemblies
               .SelectMany(x => x.GetTypes())
               .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
               .OrderBy(x => x.Name)
               .Select(Activator.CreateInstance)
               .Cast<IModule>()
               .ToList();
    }
}

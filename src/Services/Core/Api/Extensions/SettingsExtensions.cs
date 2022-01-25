using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetUnderControl.API.Extensions
{
    public static class SettingsExtensions
    {
        public static T ConfigureSettings<T>(this IConfiguration configuration, IServiceCollection services, bool addToScope = true) where T : class, new()
        {
            var sectionName = typeof(T).Name.Replace("Settings", string.Empty);
            var configurationValue = new T();
            var section = configuration.GetSection(sectionName);
            section.Bind(configurationValue);

            if(addToScope)
            {
                services.AddConfigurationToScope<T>(section);
            }
         
            return configurationValue;
        }

        public static void AddConfigurationToScope<T>(this IServiceCollection services, IConfigurationSection section) where T : class, new()
        {
            services.Configure<T>(section);
            services.AddScoped(cfg => cfg.GetService<IOptionsSnapshot<T>>().Value);
        }
    }
}

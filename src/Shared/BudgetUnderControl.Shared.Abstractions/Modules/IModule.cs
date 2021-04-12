using Autofac;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BudgetUnderControl.Shared.Abstractions.Modules
{
    public interface IModule
    {
        string Name { get; }
        string Path { get; }
        void Register(IServiceCollection services);
        void Use(IApplicationBuilder app);
        void ConfigureContainer(ContainerBuilder builder, IConfiguration configuration);
    }
}

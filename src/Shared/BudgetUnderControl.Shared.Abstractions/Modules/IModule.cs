using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BudgetUnderControl.Shared.Abstractions.Modules
{
    public interface IModule
    {
        string Name { get; }
        string Path { get; }
        void Register(IServiceCollection services, IConfiguration configuration);
        void Use(IApplicationBuilder app);
        void ConfigureContainer(ContainerBuilder builder, IConfiguration configuration);
    }
}

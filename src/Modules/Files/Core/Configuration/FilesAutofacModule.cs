using Autofac;
using BudgetUnderControl.Modules.Files.Core.Configuration.Mediation;
using BudgetUnderControl.Modules.Files.Core.Configuration.Processing;
using BudgetUnderControl.Shared.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace BudgetUnderControl.Modules.Files.Core.Configuration
{
    public class FilesAutofacModule: Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public FilesAutofacModule(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FilesModuleExecutor>()
                .As<IFilesModule>()
                .InstancePerLifetimeScope();

          
            var typeNamesEndings = new string[] { "Repository", "Service", "Builder" };

            foreach (var typeNameEnding in typeNamesEndings)
            {
                builder.RegisterAssemblyTypes(Assemblies.Core)
                    .Where(type => type.Name.EndsWith(typeNameEnding))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope()
                    .FindConstructorsWith(new AllConstructorFinder());
            }
            
            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new ProcessingModule());

        }

    }
}

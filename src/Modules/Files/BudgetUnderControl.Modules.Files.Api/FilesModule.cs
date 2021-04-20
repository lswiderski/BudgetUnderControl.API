
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Autofac;
using BudgetUnderControl.Modules.Files.Core;
using BudgetUnderControl.Modules.Files.Core.Commands.Files.DeleteFile;
using BudgetUnderControl.Modules.Files.Core.Commands.Files.GetFile;
using BudgetUnderControl.Modules.Files.Core.Commands.Files.GetFileContent;
using BudgetUnderControl.Modules.Files.Core.Commands.Files.SaveFile;
using BudgetUnderControl.Modules.Files.Core.Configuration;
using BudgetUnderControl.Modules.Files.Core.DTO;
using BudgetUnderControl.Shared.Abstractions.Modules;
using BudgetUnderControl.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("BudgetUnderControl.API")]
namespace BudgetUnderControl.Modules.Files.Api
{
    internal class FilesModule: IModule
    {
        public const string BasePath = "files-module";
        public string Name { get; } = "Files";
        public string Path => BasePath;

        public void ConfigureContainer(ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterModule(new FilesAutofacModule(configuration));
        }

        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCore(configuration["filesModule:database:ConnectionString"]);
        }

        public void Use(IApplicationBuilder app)
        {
            app.UseCore();
            
            app.UseModuleRequests()
                .Subscribe<GetFileContentQuery, byte[]>("files/getContent",
                    (query, sp) => sp.GetRequiredService<IFilesModule>().ExecuteQueryAsync(query))
                .Subscribe<GetFilesQuery, List<FileDto>>("files/getMany",
                    (query, sp) => sp.GetRequiredService<IFilesModule>().ExecuteQueryAsync(query))
                .Subscribe<DeleteFileCommand, object>("files/delete",
                    async  (query, sp) =>
                    {
                        await sp.GetRequiredService<IFilesModule>().ExecuteCommandAsync(query);
                return null;
            })
                .Subscribe<SaveFileCommand, object>("files/create",
                    async (query, sp) =>
                    {
                        await  sp.GetRequiredService<IFilesModule>().ExecuteCommandAsync(query);
                        return null;
                    });
            
        }
    }
}

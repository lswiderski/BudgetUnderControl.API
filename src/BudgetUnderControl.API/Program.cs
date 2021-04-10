using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

namespace BudgetUnderControl.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
           .ConfigureLogger()
           .CreateBootstrapLogger();

            try
            {
                Log.Information("Starting up");

                var host = Host.CreateDefaultBuilder(args)
                    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .UseSerilog((context, services, configuration) => configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .ConfigureLogger()
                        )
                    .ConfigureWebHostDefaults(webHostBuilder =>
                    {
                        webHostBuilder
                         .UseUrls("http://*:5000", "http://*:45455")
                       .UseStartup<Startup>();
                    })
                    .Build();

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

    }

    public static class LoggerExtension
    {
        public static LoggerConfiguration ConfigureLogger(this LoggerConfiguration configuration)
        {
            configuration
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(new CompactJsonFormatter(), "logs/logs-.txt", rollingInterval: RollingInterval.Day);

            return configuration;
        }
    }
}

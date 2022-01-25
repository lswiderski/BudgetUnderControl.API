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

        public static IHostBuilder CreateWebHostBuilder(string[] args)
           => Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                    .UseSerilog((context, services, configuration) => configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .ConfigureLogger()
                        )
                    .ConfigureWebHostDefaults(webHostBuilder =>
                    {
                        webHostBuilder
                            .UseStartup<Startup>();
                    });


        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
           .ConfigureLogger()
           .CreateBootstrapLogger();

            try
            {
                Log.Information("Starting up");

                using var host = CreateWebHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception ex)
            {
                if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
                {
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        .CreateLogger();
                }

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

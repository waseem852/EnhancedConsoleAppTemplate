//using ConsoleUI.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            var config = builder.Build();


            //Serilog Config. The Configurations will be retrieved from appsettings.json
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                //.Enrich.FromLogContext()
                //.WriteTo.Console()
                .CreateLogger();

            //Logging the Application Start
            Log.Logger.Information("Application Starting");

            //EXAMPLE READ SECTION - "EXAMPLESETTINGS"
            //var exampleConfig = config.GetSection(nameof(ExampleSettings)).Get<ExampleSettings>();




            //DI
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    //Demo Class Injection
                    services.AddTransient<IGreetingService, GreetingService>();


                    //Example Config
                    //services.AddSingleton(exampleConfig);


                })
                .UseSerilog() // Serilog
                .Build();

            //Calling the service method.
            var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
            svc.Run();

        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Production}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}

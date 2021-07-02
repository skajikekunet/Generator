using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using generator.Host;
using generator.Log;
using generator.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceStack.Text;
using System;
using System.IO;

namespace generator
{


    class Program
    {
        private static string _сonfigFile = "appsettings.json";
       
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args);
            using (host)
            {
                host.Start();
                host.WaitForShutdown();
            }

        }

        public static IHost CreateHostBuilder(string[] args) => new HostBuilder()
                  .ConfigureHostConfiguration(configurationBuilder =>
                  {
                      configurationBuilder  
                        .AddEnvironmentVariables("DOTNET_");
                  })
                  .ConfigureAppConfiguration((context, configurationBuilder) =>
                  {
                      configurationBuilder
                        .AddJsonFile(_сonfigFile)
                        .AddEnvironmentVariables();
                  })

                  .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                  .ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
                  {
                      containerBuilder.RegisterModule(new ConfigurationModule(context.Configuration));

                      containerBuilder.RegisterModule(new ExcelModule());
                      containerBuilder.RegisterModule(new ConverterModule());
                  })
                  .UseContentRoot(Directory.GetCurrentDirectory())

                  .ConfigureLogging((context, logging) =>
                  {
                      logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                      logging.ClearProviders();
                      logging.AddConsole();
                      logging.AddProvider(new FileLoggerProvider("log.txt"));
                  })
                   .ConfigureServices((context, services) =>
                   {
                     services.AddHostedService<MainHostService>();
                   })
                   .Build();

    }
}

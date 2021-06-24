using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using generator.Host;
using generator.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.IO;

namespace generator
{


    class Program
    {
        private static string ConfigFile = "appsettings.json";
       
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args);
            using (host)
            {
                host.Start();
                host.WaitForShutdown();
            }

        }

        public static IHost CreateHostBuilder(string[] args)
        {
            return new HostBuilder()
                  .ConfigureHostConfiguration(configurationBuilder =>
                  {
                      configurationBuilder
                       .AddEnvironmentVariables("DOTNET_");
                  })
               
                  .ConfigureAppConfiguration((context, configurationBuilder) =>
                  {
                      configurationBuilder
                        .AddJsonFile(ConfigFile)
                        .AddEnvironmentVariables();
                  })

                  .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                  .ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
                  {
                      containerBuilder.RegisterModule(new ConfigurationModule(context.Configuration));
                      containerBuilder.RegisterModule(new ExcelModule());
                  })
                  .UseContentRoot(Directory.GetCurrentDirectory())
          
                  .ConfigureLogging((context, logging) =>
                  {
                     var env = context.HostingEnvironment;
                      logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                      
                      logging.ClearProviders();
                      logging.AddEventSourceLogger();
                      logging.AddDebug();
                      
  
                  })
                   .ConfigureServices((context, services) =>
                 {
                     services.AddHostedService<MainHostService>();
                 })
                   .Build();
        }

    }
}

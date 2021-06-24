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
                     /* containerBuilder.RegisterModule(new TemplateModule());
                      containerBuilder.RegisterModule(new ServicesModule());*/

                  })
                  .UseContentRoot(Directory.GetCurrentDirectory())
          
                  .ConfigureLogging((context, logging) =>
                  {
                     var env = context.HostingEnvironment;
                      logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                      
                      logging.ClearProviders();
                      logging.AddEventSourceLogger();
                      logging.AddDebug();
                      
                  /*     if (context.HostingEnvironment.IsDevelopment())
                         {

                             logging.AddNLog($"nlog.{env.EnvironmentName}.config");
                         }*/
                  })
                   .ConfigureServices((context, services) =>
                 {
                     services.AddHostedService<MainHostService>();
                 })
                   .Build();
        }

   /* #region Процесс генерации
    private static void Start()
        {
            var options = new Options(Configuration);
            var proc = new Event(Configuration);
                
                for (int i = 0; i < options.CountJournals; i++)
                {
                    proc.FileName = Guid.NewGuid().ToString();//Названия журнала
                    Console.WriteLine("Журнал: " + proc.FileName);
                    for (int j = 0; j < options.CountFiles; j++)
                    {
                        using (var file = new StreamWriter($"{Configuration["OutputPath"]}\\{proc.FileName}#{j}.slog"))
                        {
                            file.WriteLine(proc.Head); //Заголовог файла
                            for (int k = 0; k <options.CountEvents; k++)
                            {
                                file.WriteLine(proc.GetEvent); // Вывести событие
                                TotalCountEvents++;
                            }
                            file.WriteLine(proc.Tail); //Написать хвост файла
                            proc.ChangeFile(); // Сменить файл
                            file.Close();
                            Console.WriteLine($"\t Создан файл: #{j,3} \t кол-во событий: {options.CountEvents}");
                        }
                        
                    }
                    proc.ChangeLog(); // Сменить журнал
                }
          
        }
        #endregion

        #region Статистика
        private static void GetStat() 
        {
            Console.WriteLine("Общее количество сгенерированных событий: " + TotalCountEvents);
            Console.WriteLine("rs:");
            foreach (Pattern i in Arrays.Rs.Values)
            {
                Console.WriteLine($"{i.Inc,4} \t {i.name}" );
            }

            Console.WriteLine("Users:");
            foreach (Pattern i in Arrays.Users.Values)
            {
                Console.WriteLine($"{i.Inc,4} \t {i.name}");
            }

            Console.WriteLine("qa:");
            foreach (Pattern i in Arrays.Qa.Values)
            {
                Console.WriteLine($"{i.Inc,4} \t {i.name}");
            }

            Console.WriteLine("MachineName:");
            foreach (Pattern i in Arrays.MachineName.Values)
            {
                Console.WriteLine($"{i.Inc,4} \t {i.name}");
            }
        }
        #endregion */

    }
}

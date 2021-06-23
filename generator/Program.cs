using Autofac;
using generator.Static;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace generator
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Excel>()
                            .As<IExcel>()
                           .SingleInstance();

        }
    }

    class Program
    {
        private static string ConfigFile = "appsettings.json";

        private static int TotalCountEvents = 0;

        private static IContainer Excel { get; set; }

        private static IConfiguration Configuration;
        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigFile, optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

            Excel = AutofacConfig.ConfigureExcel(Configuration["Excel"]);
            using (var scope = Excel.BeginLifetimeScope())
            {
                var writer = scope.Resolve<IExcel>();
                writer.LoadInfo(Configuration["Excel"]);
                Console.WriteLine(writer.ErrorRead);
                
            }
                     /*   Excel.LoadInfo(Configuration["Excel"]);
                        if (!Excel.ErrorRead)
                        {
                            Start();
                            GetStat();
                        }*/
        }

        #region Процесс генерации
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
        #endregion 

    }
}

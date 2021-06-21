using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using Excel = generator.Excel_Load;

namespace generator
{
    class Program
    {
        //private IConfiguration Configuration;
        private static string ConfigFile = "appsettings.json";

       // private static GenerationFiles files;

        private static string path = "";
        private static int TotalCountEvents = 0;

        private static IConfiguration Configuration;
        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigFile, optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();

            Excel.GetInfo(Configuration["Excel"]);
     
            Start();
            GetStat();
        }

        #region Процесс генерации
        private static void Start()
        {
            int countJ; //Кол-во журналов
            int countFiles; //Кол-во файлов
            int minEvents = 500;
            int maxEvents = 500;
            int step = 1;

            int.TryParse(Configuration["minEvents"], out minEvents);
            int.TryParse(Configuration["maxEvents"], out maxEvents);
            int.TryParse(Configuration["step"], out step);

            Event proc;
            path = Configuration["outputPath"];
            if (int.TryParse(Configuration["journal"], out countJ) && int.TryParse(Configuration["journalFile"], out countFiles) )
            {
                proc = new Event(Configuration["errChance"], Configuration["repeatChance"], Configuration["MachineName"], Configuration["User"], Configuration["n"], Configuration["kind"], 
                            Configuration["CommandLine"], Configuration["dn"], Configuration["dd"], Configuration["qa"], Configuration["ProcessName"], Configuration["timeEventMin"], Configuration["timeEventMax"],
                             Configuration["minInn"], Configuration["maxInn"], Configuration["minFid"], Configuration["maxFid"]);
               

                for (int i = 0; i < countJ; i++)
                {
                    string fileName = Guid.NewGuid().ToString();//Названия журнала
                    Console.WriteLine("Журнал: " + fileName);

                    proc.FileName = fileName;
                    for (int j = 0; j < countFiles; j++)
                    {
                        var file = new StreamWriter($"{path}\\{fileName}#{j}.slog");
                        file.WriteLine(proc.Head); //Заголовог файла
                        int countEvents;
                        if (minEvents < maxEvents)
                            countEvents = (int)Math.Floor((double)(new Random().Next(minEvents, maxEvents + 1) / step)) * step;
                        else
                            countEvents = minEvents;
                        
                        for (int k = 0; k < countEvents; k++)
                        {
                            file.WriteLine(proc.GetEvent); // Вывести событие
                            TotalCountEvents++;
                        }
                        file.WriteLine(proc.Tail); //Написать хвост файла
                        proc.ChangeFile(); // Сменить файл
                        file.Close();
                        Console.WriteLine($"\t Создан файл: #{j,3} \t кол-во событий: {countEvents}");
                    }
                    proc.ChangeLog(); // Сменить журнал
                }
            }
            else
            {
                Console.WriteLine("Ошибка конфигурации");
            }
        }
        #endregion

        #region Статистика
        private static void GetStat() 
        {
            Console.WriteLine("Общее количество сгенерированных событий: " + TotalCountEvents);
            Console.WriteLine("rs:");
            foreach (Pattern i in Excel.Rs.Values)
            {
                Console.WriteLine($"{i.name} \t {i.Inc}");
            }

            Console.WriteLine("sp:");
            foreach (Pattern i in Excel.Sp.Values)
            {
                Console.WriteLine($"{i.name} \t {i.Inc}");
            }
        }
        #endregion 

    }
}

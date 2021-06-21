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
            int minFiles; //Кол-во файлов
            int maxFiles;
            int minEvents;
            int maxEvents;
            int step;
            int countFiles;

            int.TryParse(Configuration["minEvents"], out minEvents);
            int.TryParse(Configuration["maxEvents"], out maxEvents);
            int.TryParse(Configuration["step"], out step);

            int.TryParse(Configuration["journal"], out countJ);
            int.TryParse(Configuration["minjJournalFile"], out minFiles);
            int.TryParse(Configuration["maxjJournalFile"], out maxFiles);
            
            

            Event proc;
            path = Configuration["outputPath"];

                proc = new Event(Configuration);
                for (int i = 0; i < countJ; i++)
                {
                    string fileName = Guid.NewGuid().ToString();//Названия журнала
                    Console.WriteLine("Журнал: " + fileName);

                    proc.FileName = fileName;
                    countFiles = new Random().Next(minFiles, maxFiles + 1);

                    for (int j = 0; j < countFiles; j++)
                    {
                        using (var file = new StreamWriter($"{path}\\{fileName}#{j}.slog"))
                        {
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

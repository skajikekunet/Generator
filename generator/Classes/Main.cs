using generator.Interfaces;
using generator.Interfaces.Templates;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace generator.Classes
{
    class Main: IMain
    {
        private readonly IConfiguration Configuration;
        private readonly IExcel _excel;

        private IEvent _event;
        private IOption _option;

        private int TotalCountEvents = 0;

        public Main(IConfiguration config, IExcel excel)
        {
            Configuration = config;
            _excel = excel;
            _event = new Event(config, excel);
            _option = new Options(config);
        }
        #region Процесс генерации
        public void Start()
        {
            

            for (int i = 0; i < _option.CountJournals; i++)
            {
                _event.FileName = Guid.NewGuid().ToString();//Названия журнала
                Console.WriteLine("Журнал: " + _event.FileName);
                for (int j = 0; j < _option.CountFiles; j++)
                {
                    using (var file = new StreamWriter($"{Configuration["OutputPath"]}\\{ _event.FileName}#{j}.slog"))
                    {
                        file.WriteLine(_event.Head); //Заголовог файла
                        for (int k = 0; k < _option.CountEvents; k++)
                        {
                            file.WriteLine(_event.GetEvent); // Вывести событие
                            TotalCountEvents++;
                        }
                        file.WriteLine(_event.Tail); //Написать хвост файла
                        _event.ChangeFile(); // Сменить файл
                        file.Close();
                        Console.WriteLine($"\t Создан файл: #{j,3} \t кол-во событий: {_option.CountEvents}");
                    }

                }
                _event.ChangeLog(); // Сменить журнал
            }

        }
        #endregion

        #region Статистика
        public void GetStat()
        {
            Console.WriteLine("Общее количество сгенерированных событий: " + TotalCountEvents);
            Console.WriteLine("rs:");
            foreach (Pattern i in _excel.Rs.Values)
            {
                Console.WriteLine($"{i.Inc,4} \t {i.name}");
            }

            Console.WriteLine("Users:");
            foreach (Pattern i in _excel.Users.Values)
            {
                Console.WriteLine($"{i.Inc,4} \t {i.name}");
            }

            Console.WriteLine("qa:");
            foreach (Pattern i in _excel.Qa.Values)
            {
                Console.WriteLine($"{i.Inc,4} \t {i.name}");
            }

            Console.WriteLine("MachineName:");
            foreach (Pattern i in _excel.MachineName.Values)
            {
                Console.WriteLine($"{i.Inc,4} \t {i.name}");
            }
        }
        #endregion 

    }
}


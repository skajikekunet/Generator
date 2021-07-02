using generator.Host;
using generator.Interfaces;
using generator.Interfaces.Templates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace generator.Classes
{
    class Main: IMain
    {
        private readonly IConfiguration _сonfiguration;
        private readonly IExcel _excel;
        private readonly ILogger<MainHostService> _log;

        private IEvent _event;
        private IOption _option;

        private int TotalCountEvents = 0;

        public Main(IConfiguration config, IExcel excel, IConverter converter, ILogger<MainHostService> log)
        {
            _log = log;
            _сonfiguration = config;
            _excel = excel;
            _event = new Event(config, excel, converter);
            _option = new Options(config, converter);
        }
        #region Процесс генерации
        public void Start()
        {
            for (int i = 0; i < _option.CountJournals; i++)
            {
                _event.FileName = Guid.NewGuid().ToString();//Названия журнала
                _log.LogInformation("Журнал: " + _event.FileName);

                if (!Directory.Exists(_сonfiguration["OutputPath"])) Directory.CreateDirectory(_сonfiguration["OutputPath"]);

                for (int j = 0; j < _option.CountFiles; j++)
                {
                    try
                    {
                        using (var file = new StreamWriter($"{_сonfiguration["OutputPath"]}\\{ _event.FileName}#{j}.slog"))
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
                            _log.LogInformation($"\t Создан файл: #{j,3} \t кол-во событий: {_option.CountEvents}");
                        }
                    }
                    catch { }

                }
                _event.ChangeLog(); // Сменить журнал
            }

        }
        #endregion

        #region Статистика
        public void GetStat()
        {
            _log.LogInformation("Общее количество сгенерированных событий: " + TotalCountEvents);
            _log.LogInformation("rs:");
            foreach (Pattern i in _excel.Rs.Values)
            {
                _log.LogInformation($"{i.Inc,4} \t {i.name}");
            }

            _log.LogInformation("Users:");
            foreach (Pattern i in _excel.Users.Values)
            {
                _log.LogInformation($"{i.Inc,4} \t {i.name}");
            }

            _log.LogInformation("qa:");
            foreach (Pattern i in _excel.Qa.Values)
            {
                _log.LogInformation($"{i.Inc,4} \t {i.name}");
            }

            _log.LogInformation("MachineName:");
            foreach (Pattern i in _excel.MachineName.Values)
            {
                _log.LogInformation($"{i.Inc,4} \t {i.name}");
            }
        }
        #endregion 

    }
}


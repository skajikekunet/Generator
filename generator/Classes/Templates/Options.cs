using generator.Interfaces.Templates;
using Microsoft.Extensions.Configuration;
using System;

namespace generator
{
    class Options: IOption //основные параметры конфига
    {
        private int countJ = 0; //Кол-во журналов
        private int minFiles = 0; //Кол-во файлов
        private int maxFiles = 0;

        private int minEvents = 0;
        private int maxEvents = 0;
        private int step = 1;
        public int CountFiles { get { return new Random().Next(minFiles, maxFiles + 1); } }
        public int CountEvents { get { return (int)Math.Floor((double)(new Random().Next(minEvents, maxEvents + 1) / step)) * step; } }

        public int CountJournals { get => countJ; }


        public Options(IConfiguration config)
        {
            countJ = Converter.ConverToInt(config["Journal"]);
            minFiles = Converter.ConverToInt(config["MinJournalFile"]);
            maxFiles = Converter.ConverToInt(config["MaxJournalFile"]);

            minEvents = Converter.ConverToInt(config["MinEvents"]);
            maxEvents = Converter.ConverToInt(config["MaxEvents"]);
            step = Converter.ConverToInt(config["Step"]);
        }
    }
}

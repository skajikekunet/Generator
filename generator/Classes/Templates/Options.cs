using generator.Interfaces;
using generator.Interfaces.Templates;
using Microsoft.Extensions.Configuration;
using System;

namespace generator
{
    class Options: IOption //основные параметры конфига
    {
        private int _countJ = 0; //Кол-во журналов
        private int _minFiles = 0; //Кол-во файлов
        private int _maxFiles = 0;

        private int _minEvents = 0;
        private int _maxEvents = 0;
        private int _step = 1;
        public int CountFiles { get { return new Random().Next(_minFiles, _maxFiles + 1); } }
        public int CountEvents { get { return (int)Math.Floor((double)(new Random().Next(_minEvents, _maxEvents + 1) / _step)) * _step; } }

        public int CountJournals { get => _countJ; }


        public Options(IConfiguration config, IConverter converter)
        {
            _countJ = converter.ConverToInt(config["Journal"]);
            _minFiles = converter.ConverToInt(config["MinJournalFile"]);
            _maxFiles = converter.ConverToInt(config["MaxJournalFile"]);

            _minEvents = converter.ConverToInt(config["MinEvents"]);
            _maxEvents = converter.ConverToInt(config["MaxEvents"]);
            _step = converter.ConverToInt(config["Step"]);
        }
    }
}

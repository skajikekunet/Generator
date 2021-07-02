using generator.Interfaces;
using generator.Interfaces.Templates;
using Microsoft.Extensions.Configuration;
using System;

namespace generator
{
    class FileTemplate: IFileTemplate
    {
        
        public string ProcessName { get => _processName; }
        public string N { get => _n; }
        public string Kind { get => _kind; }

        public string CommandLine { get => _commandLine; }
        public string Dn { get => _dn; }
        public string Dd { get => _dd; }
        public string Time { get => AddTime(); } // время создания события time;



        private string _processName;
        private string _n;
        private string _kind;

        private string _commandLine;
        private string _dn;
        private string _dd;
        private int _eventTimeMin;
        private int _eventTimeMax;
        private long _time;

        public FileTemplate(IConfiguration configuration, IConverter converter )
        {
            _n = configuration["N"];
            _kind = configuration["Kind"];
            _commandLine = configuration["CommandLine"];
            _dn = configuration["Dn"];
            _dd = configuration["Dd"];
            _processName = configuration["ProcessName"];

            _eventTimeMin = converter.ConverToInt(configuration["TimeEventMin"]);
            _eventTimeMax = converter.ConverToInt(configuration["TimeEventMax"]);

            _time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public string AddTime()
        {
            _time +=new Random().Next(_eventTimeMin, _eventTimeMax + 1);
            return "" + _time;
        }

    }
}

using generator.Interfaces.Templates;
using Microsoft.Extensions.Configuration;
using System;

namespace generator
{
    class FileTemplate: IFileTemplate
    {
        
        public string ProcessName { get; }
        public string N { get; }
        public string Kind { get; }

        public string CommandLine { get; }
        public string Dn { get; }
        public string Dd { get; }
        public string Time { get => AddTime(); } // время создания события time;



        private string processName;
        private string n;
        private string kind;

        private string commandLine;
        private string dn;
        private string dd;
        private int eventTimeMin;
        private int eventTimeMax;
        private long time;

        public FileTemplate(IConfiguration Configuration)
        {
            n = Configuration["N"];
            kind = Configuration["Kind"];
            commandLine = Configuration["CommandLine"];
            dn = Configuration["Dn"];
            dd = Configuration["Dd"];
            processName = Configuration["ProcessName"];

            eventTimeMin = Converter.ConverToInt(Configuration["TimeEventMin"]);
            eventTimeMax = Converter.ConverToInt(Configuration["TimeEventMax"]);

            time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        public string AddTime()
        {
            time +=new Random().Next(eventTimeMin, eventTimeMax + 1);
            return "" + time;
        }

    }
}

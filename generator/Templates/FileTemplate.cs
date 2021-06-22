using Microsoft.Extensions.Configuration;
using System;

namespace generator
{
    class FileTemplate
    {
        
        public string ProcessName;
        public string N;
        public string Kind;
        
        public string CommandLine;
        public string Dn;
        public string Dd;
        public string Time { get => AddTime(); } // время создания события time;
    

        private int eventTimeMin;
        private int eventTimeMax;
        private long time;

        public FileTemplate(IConfiguration Configuration)
        {
            N = Configuration["N"];
            Kind = Configuration["Kind"];
            CommandLine = Configuration["CommandLine"];
            Dn = Configuration["Dn"];
            Dd = Configuration["Dd"];
            ProcessName = Configuration["ProcessName"];

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

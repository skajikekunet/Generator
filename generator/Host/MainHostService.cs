using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using generator.Classes;
using generator.Interfaces;
using generator.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace generator.Host
{
    class MainHostService: BackgroundService
    {
        private readonly IExcel _excel;
        private readonly IConfiguration _config;
        private readonly ILogger _log;
       // private IMain _main;

        public MainHostService(IExcel excel, IConfiguration config, ILogger<MainHostService> log/*, IMain main*/)
        {
            _excel = excel;
            _config = config;
            _log = log;
           // _main = main;
           // log.LogDebug("инфо13");
        }

        

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _excel.LoadInfo(_config["Excel"]);


             if (!_excel.ErrorRead)
             {
                var _main = new Main(_config,_excel);
                    _main.Start();
                    _main.GetStat();
             }
 
            return Task.CompletedTask;
        }
    }
}

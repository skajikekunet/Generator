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
    public class MainHostService: BackgroundService
    {
        private readonly IExcel _excel;
        private readonly IConfiguration _config;
      //  private readonly ILoggerFactory _logFactory;
        private readonly IConverter _converter;
        private readonly ILogger<MainHostService> _log;
        // private IMain _main;

        public MainHostService(IExcel excel, IConfiguration config, IConverter converter, ILogger<MainHostService> log)
        {
            _excel = excel;
            _config = config;
            _converter = converter;
            _log = log;
           // _log.LogInformation("jrtjgrhrgg");
           // _log.LogError("jrtjgrhrgg");
        }

        

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _excel.LoadInfo(_config["Excel"], _log);

          
            if (!_excel.ErrorRead)
            {
               var _main = new Main(_config,_excel, _converter, _log);
                   _main.Start();
                   _main.GetStat();
            }

            return Task.CompletedTask;
        }
    }
}

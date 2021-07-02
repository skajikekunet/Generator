using generator.Host;
using generator.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace generator
{
    public class Converter: IConverter
    {
        private readonly ILogger<Converter> _log;
        public Converter(ILogger<Converter> log)
        {
            _log = log;
        }

        public int ConverToInt(string str)
        {
            int num;
            if (!int.TryParse(str, out num))
            {
                _log.LogError("Ошибка конфигурации");
            }
            return num;
        }

        public int ConverToInt(string str, string name)
        {
            int num;
            if (!int.TryParse(str, out num))
            {
                _log.LogError($"Ошибка конфигурации (Параметр: {name})");
            }
            return num;
        }

        public double ConverToDouble(string str)
        {
            str = str.Replace(".", ",");
            double num;
            if (!double.TryParse(str, out num))
            {
                _log.LogError("Ошибка конфигурации");
            }
            return num;
        }

        public double ConverToDouble(string str, string name)
        {
            str = str.Replace(".", ",");
            double num;
            if (!double.TryParse(str, out num))
            {
                _log.LogError($"Ошибка конфигурации (Параметр: {name})");
            }
            return num;
        }

        public bool Random(double chance)
        {
            var r = new Random().Next(0, 100000);
            if (r < chance * 100000)
                return true;
            else
                return false;
        }
    }
}

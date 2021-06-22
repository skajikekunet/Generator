using System;
using System.Collections.Generic;
using System.Text;

namespace generator
{
    public static class Converter
    {
        public static int ConverToInt(string str)
        {
            int num;
            if (!int.TryParse(str, out num))
            {
                Console.WriteLine("Ошибка конфигурации");
            }
            return num;
        }

        public static int ConverToInt(string str, string name)
        {
            int num;
            if (!int.TryParse(str, out num))
            {
                Console.WriteLine($"Ошибка конфигурации (Параметр: {name})");
            }
            return num;
        }

        public static double ConverToDouble(string str)
        {
            str = str.Replace(".", ",");
            double num;
            if (!double.TryParse(str, out num))
            {
                Console.WriteLine("Ошибка конфигурации");
            }
            return num;
        }

        public static double ConverToDouble(string str, string name)
        {
            str = str.Replace(".", ",");
            double num;
            if (!double.TryParse(str, out num))
            {
                Console.WriteLine($"Ошибка конфигурации (Параметр: {name})");
            }
            return num;
        }

        public static bool Random(double chance)
        {
            var r = new Random().Next(0, 100000);
            if (r < chance * 100000)
                return true;
            else
                return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace generator.Interfaces
{
    public interface IConverter
    {
        int ConverToInt(string str);
        int ConverToInt(string str, string name);
        double ConverToDouble(string str);
        double ConverToDouble(string str, string name);
        bool Random(double chance);
    }
}

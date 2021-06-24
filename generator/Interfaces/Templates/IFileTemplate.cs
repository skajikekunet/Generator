using System;
using System.Collections.Generic;
using System.Text;

namespace generator.Interfaces.Templates
{
    interface IFileTemplate
    {
        string ProcessName { get; }
        string N { get; }
        string Kind { get; }

        string CommandLine { get; }
        string Dn { get; }
        string Dd { get; }
        string Time { get; }
    }
}

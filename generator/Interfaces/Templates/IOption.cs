using System;
using System.Collections.Generic;
using System.Text;

namespace generator.Interfaces.Templates
{
    interface IOption
    {
        int CountFiles { get; }
        int CountEvents { get; }

        int CountJournals { get; }
    }
}

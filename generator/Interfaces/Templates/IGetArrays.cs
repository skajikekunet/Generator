using System;
using System.Collections.Generic;
using System.Text;

namespace generator.Interfaces.Templates
{
    interface IGetArrays
    {
        string Inn { get; }
        string Fid { get; }
        string Rs { get; }
        string Sp { get; }
        bool onlyStatus { get; set; }
    }
}

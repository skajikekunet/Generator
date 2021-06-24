using System;
using System.Collections.Generic;
using System.Text;
using static generator.Excel;

namespace generator.Interfaces
{
    public interface IExcel
    {
        void LoadInfo(string path);
        bool ErrorRead { get;}

        string[] Inn { get; set; }
        string[] Fid { get; set; }
        Dictionary<int, Pattern> Rs { get; set; }
        Dictionary<int, Pattern> MachineName { get; set; }
        Dictionary<int, Pattern> Users { get; set; }
        Dictionary<int, Pattern> Ssid { get; set; }
        Dictionary<int, Pattern> Qa { get; set; }
        Dictionary<int, Pattern> Sp { get; set; }
    }
}

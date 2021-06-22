using System;
using System.Collections.Generic;
using System.Text;

namespace generator.Static
{
    public class Pattern
    {
        public string Name { get { Inc++; return name; } }
        public string name;
        public int Inc = 0;
        public int Ssid = 0;

        public Pattern(string _name)
        {
            name = _name;
        }
        public Pattern(string _name, int Ssid)
        {
            name = _name;
        }
    }

    public static class Arrays
    {

        public static string[] Inn;
        public static string[] Fid;
        public static Dictionary<int, Pattern> Rs;
        public static Dictionary<int, Pattern> MachineName;
        public static Dictionary<int, Pattern> Users;
        public static Dictionary<int, Pattern> Ssid;
        public static Dictionary<int, Pattern> Qa;
        public static Dictionary<int, Pattern> Sp;

        public static void Add(OfficeOpenXml.ExcelRange cells)
        {
            Rs = new Dictionary<int, Pattern>();
            MachineName = new Dictionary<int, Pattern>();
            Sp = new Dictionary<int, Pattern>();
            Users = new Dictionary<int, Pattern>();
            Ssid = new Dictionary<int, Pattern>();
            Qa = new Dictionary<int, Pattern>();

            for (var row = 3; row <= 7; row++)
            {
                var i = 2;
                while (cells[i, row].Value != null)
                {
                    switch (row)
                    {
                        case 3:
                            Rs.Add(i - 2, new Pattern(cells[i, row].Value.ToString()));
                            break;
                        case 4:
                            MachineName.Add(i - 2, new Pattern(cells[i, row].Value.ToString()));
                            break;
                        case 5:
                            Users.Add(i - 2, new Pattern(cells[i, row].Value.ToString()));
                           // Console.WriteLine(Users[i - 2].Name);
                            break;
                        case 6:
                            Ssid.Add(i - 2, new Pattern(cells[i, row].Value.ToString()));
                            break;
                        case 7:
                            Qa.Add(i - 2, new Pattern(cells[i, row].Value.ToString()));
                            break;
                    }
                    i++;
                }
            }
        }

    }
}

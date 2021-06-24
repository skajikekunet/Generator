
using Autofac;
using generator.Interfaces;
using generator.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace generator
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


    public class Excel: IExcel
    {
        //public bool errorRead = false; //ошибка чтения файла
        public bool ErrorRead { get => errorRead;}
        private bool errorRead = true;

        public string[] Inn { get; set; }
            public string[] Fid { get; set; }
            public Dictionary<int, Pattern> Rs { get; set; }
            public Dictionary<int, Pattern> MachineName { get; set; }
            public Dictionary<int, Pattern> Users { get => users; set => users = value; }
            public Dictionary<int, Pattern> Ssid { get; set; }
            public Dictionary<int, Pattern> Qa { get; set; }
            public Dictionary<int, Pattern> Sp { get; set; }

        private Dictionary<int, Pattern> users;


      
   

        private void Add(OfficeOpenXml.ExcelRange cells)
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
                    Console.WriteLine("" + cells[i, row].Value);
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

            public void LoadInfo(string path) //Загрузка массивов inn, fid, rs, sp
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (Stream file = new FileStream(path, FileMode.Open))
            {
                Console.WriteLine("Процесс считывания данных с Excel");
                try
                {
                    ExcelPackage package = new ExcelPackage(file);
                    ExcelWorksheet sheet = package.Workbook.Worksheets[0];

                    #region Размер массивов inn и fid
                    var inn_size = 2;
                    var fid_size = 2;
                    while (sheet.Cells[inn_size, 1].Value != null)
                    {
                        inn_size++;
                    }

                    while (sheet.Cells[fid_size, 2].Value != null)
                    {
                        fid_size++;
                    }
                    Inn = new string[--inn_size];
                    Fid = new string[--fid_size];
                    #endregion

                    Add(sheet.Cells);
               

                   for (int i = 0; i < inn_size - 1; i++)
                    {
                        Inn[i] = sheet.Cells[i + 2, 1].Value.ToString();
                        // Console.WriteLine(Arrays.Inn[i]);
                    }

                    for (int i = 0; i < fid_size - 1; i++)
                    {
                        Fid[i] = sheet.Cells[i + 2, 2].Value.ToString();
                        
                    }

                    file.Close();
                    Console.WriteLine("Данные считаны");
                    errorRead = false;

                  /*  _host.ConfigureContainer<ContainerBuilder>((context, containerBuilder) =>
                    {
                        containerBuilder.RegisterModule(new TemplateModule());
                        containerBuilder.RegisterModule(new ServicesModule());

                    });*/
                } catch
                {
                    Console.WriteLine("Ошибка считывания данных с Excel");
                }
                finally
                {
                    if (file != null) file.Close();
                }
            }
        
        }

        private void CheckErrors()
        {
            if (Ssid.Count != Users.Count)
            {
                Console.WriteLine("Ошикба чтения данных с Excel");
                errorRead = true;
            }
        }
    }
}

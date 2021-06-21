
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace generator
{
    public class Pattern
    {
        public string Name { get { Inc++; return name; }  }
        public string name;
        public int Inc = 0;

        public Pattern(string _name)
        {
            name = _name;
        }
    }

    public static class Excel_Load
    {
       
        public static string[] Inn { get => inn; }
        public static string[] Fid { get => fid; }

        public static Dictionary<int, Pattern> Rs { get => GetRs(); }
        public static Dictionary<int, Pattern> Sp { get => GetSp(); }

        private static string[] inn;
        private static string[] fid;
        private static Dictionary<int, Pattern> rs;
        private static Dictionary<int, Pattern> sp;

        private static Dictionary<int, Pattern> GetRs()
        {
            if (rs.Count > 0)
                return rs;
            else
                return new Dictionary<int, Pattern>
                {
                    { 0, new Pattern("CATALOG_NAME") }
                };
        }

        private static Dictionary<int, Pattern> GetSp()
        {
            if (sp.Count > 0)
                return sp;
            else
                return new Dictionary<int, Pattern>
                {
                    {0, new Pattern("ServicePoint_A") }
                };
        }

        public static void GetInfo(string path) //Загрузка массивов inn, fid, rs, sp
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
                    inn = new string[--inn_size];
                    fid = new string[--fid_size];
                    #endregion

                    var rs_size = 2;
                    var sp_size = 2;
                    rs = new Dictionary<int, Pattern>();
                    sp = new Dictionary<int, Pattern>();

                    while (sheet.Cells[rs_size, 3].Value != null)
                    {
                        rs.Add(rs_size-2, new Pattern(sheet.Cells[rs_size, 3].Value.ToString()));
                        rs_size++;
                    }

                    while (sheet.Cells[sp_size, 4].Value != null)
                    {
                        sp.Add(sp_size-2, new Pattern(sheet.Cells[sp_size, 4].Value.ToString()));
                        sp_size++;
                    }

                    for (int i = 0; i < inn_size - 1; i++)
                        inn[i] = sheet.Cells[i + 2, 1].Value.ToString();

                    for (int i = 0; i < fid_size - 1; i++)
                        fid[i] = sheet.Cells[i + 2, 2].Value.ToString();

                    file.Close();
                    Console.WriteLine("Данные считаны");
                } catch()
                {
                    Console.WriteLine("Ошибка считывания данных с Excel");
                }
                finally
                {
                    if (file != null) file.Close();
                }
            }
        
        }
    }
}

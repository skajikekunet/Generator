
using generator.Static;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace generator
{

    public static class Excel_Load
    {
        public static bool ErrorRead = false; //ошибка чтения файла

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
                    Arrays.Inn = new string[--inn_size];
                    Arrays.Fid = new string[--fid_size];
                    #endregion

                    Arrays.Add(sheet.Cells);
               

                   for (int i = 0; i < inn_size - 1; i++)
                    {
                        Arrays.Inn[i] = sheet.Cells[i + 2, 1].Value.ToString();
                        // Console.WriteLine(Arrays.Inn[i]);
                    }

                    for (int i = 0; i < fid_size - 1; i++)
                    {
                        Arrays.Fid[i] = sheet.Cells[i + 2, 2].Value.ToString();
                        
                    }

                    file.Close();
                    Console.WriteLine("Данные считаны");
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

        private static void CheckErrors()
        {
            if (Arrays.Ssid.Count != Arrays.Users.Count)
            {
                Console.WriteLine("Ошикба чтения данных с Excel");
                ErrorRead = true;
            }
        }
    }
}

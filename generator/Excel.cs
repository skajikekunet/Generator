
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

        public static void GetInfo(string path)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

         /*   using (var package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheets sheet = package.Workbook.Worksheets;

                Console.WriteLine(sheet.Count);

            }*/
            using (Stream file = new FileStream(path, FileMode.Open))
            {
                Console.WriteLine("Процесс считывания данных с Excel");
                try
                {
                    ExcelPackage package = new ExcelPackage(file);
                    ExcelWorksheet sheet = package.Workbook.Worksheets[0];

                    var inn_size = 2;
                    var fid_size = 2;
                    // Console.WriteLine(sheet.Cells[inn_size, 1].Value.ToString());
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
                    {
                        inn[i] = sheet.Cells[i + 2, 1].Value.ToString();
                    }

                    for (int i = 0; i < fid_size - 1; i++)
                    {
                        fid[i] = sheet.Cells[i + 2, 2].Value.ToString();
                    }

/*
                    for (int i = 0; i < rs_size - 1; i++)
                    {
                        rs[i] = sheet.Cells[i + 2, 3].Value.ToString();
                         //Console.WriteLine(rs[i]);
                    }

                    for (int i = 0; i < sp_size - 1; i++)
                    {
                        sp[i] = sheet.Cells[i + 2, 4].Value.ToString();
                    }*/

                    file.Close();
                    Console.WriteLine("Данные считаны");
                } catch(Exception e)
                {
                    Console.WriteLine("Ошибка считывания данных с Excel");
                }
                finally
                {
                    if (file != null) file.Close();
                }
            }
            /*   Excel.Application ObjWorkExcel = new Excel.Application();
               Excel.Workbook ObjWorkBook;
               try
               {
                   ObjWorkBook = ObjWorkExcel.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing); //открыть файл
                                                              */                                                                                                                                                                                                          //   Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1]; //получить 1 лист

            //  var sheet = ((Excel.Worksheet)ObjWorkBook.Sheets[1]).Cells[1, i];
            // Console.WriteLine("" + sheet.Name);
            //  Excel.Range r = sheet.get_Range("A1", "A100");
            //   Console.WriteLine("" + sheet);
            //   Console.WriteLine("" + sheet.
            //  Excel.Worksheet sheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
            // var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell
            //  var range = sheet.UsedRange; 
            //Console.WriteLine("Все ок");

            //Console.WriteLine(ObjWorkSheet.Cells[2, 1].ToString());

            /* Excel.Range first = ObjWorkSheet.Range["$A:$A"];
             Excel.Range second = ObjWorkSheet.Range["$B:$B"];



             var countB = first.Count;
             var sec = first.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
             var l = 1;
             while (first[l].ToString() != "")
             {
                 Console.WriteLine(l);
                 l++;
             }
             Console.WriteLine(l);
             Console.WriteLine(sec);*/


            /*  inn = new string[lastCell.Row];
              fid = new string[lastCell.Row];

              for (int i = 1; i < lastCell.Row; i++) //lastCell.Row
              {
                  if (((Excel.Range)ObjWorkSheet.Cells[i + 1, 1]).Text.ToString() != "")
                      inn[i - 1] = ((Excel.Range)ObjWorkSheet.Cells[i + 1, 1]).Text.ToString();
                  else
                      break;
              }

              for (int i = 1; i < lastCell.Row; i++) //lastCell.Row
              {
                  if (((Excel.Range)ObjWorkSheet.Cells[i + 1, 2]).Text.ToString() != "")
                      fid[i - 1] = ((Excel.Range)ObjWorkSheet.Cells[i + 1, 2]).Text.ToString();
                  else
                      break;
              }*/



            // выйти из экселя


            /*  for (int i = lastCell.Row - 1; i > lastCell.Row - 30; i--) //lastCell.Row
              {
                  Console.WriteLine(inn[i]);
              }*/

            /*         ObjWorkBook.Close(false, Type.Missing, Type.Missing);

                 }
                 catch(Exception e) { Console.WriteLine(e); }
                 finally {
                     ObjWorkExcel.Quit();
                     System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjWorkExcel);
                     GC.Collect();
                     Console.WriteLine("Закрыл");
                 }*/
        }
    }
}

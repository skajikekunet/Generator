using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Excel = generator.Excel_Load;
namespace generator
{
    class Event
    {
        public double errorChance;
        public double repeatChance;

        private int eventTimeMin;
        private int eventTimeMax;
        private int minInn;
        private int maxInn;
        private int minFid;
        private int maxFid;

        public string GetEvent { get => Get_Event(); }
        public int GetFileIndex { get => fileIndex; }

        public string Head { get => $"LI:{filename} l.MachineName={MachineName} " +
                $"l.ProcessName={ProcessName} l.CommandLine=\"{CommandLine}\" l.Id={jIndex} " +
                $"l.StartTime=\"{DateTime.Now}\"";
        } 
        public string Tail { get => $"LI^{filename}"; } //конец файла

        private bool isRepeat = false; //есть ли повторения cnts
        private bool onlyStatus = false; //rs
        private bool canError = true; //rs

        private int ae = 0;
        private int cnts = 1;
        private int cntl = 0;

        private int fileIndex = 0;
        private int jIndex = 1;

        private string MachineName;
        private string ProcessName;
        private string User;
        private string n;
        private string kind;
        private string qa;
        private string CommandLine;
        private string un { get => $"{MachineName}\\{User}"; }
        private string dn;
        private string dd;

        private long time;

        public string FileName { get => filename; set => filename = value; }
        private string filename = "";

        private int countRepeat = 0;

        private Dictionary<int, Pattern> rs { get => Excel.Rs; }

        private Dictionary<int, Pattern> sp { get => Excel.Sp; }


        public Event(IConfiguration Configuration)
        {
            MachineName = Configuration["MachineName"];
            User = Configuration["User"];
            n = Configuration["n"];
            kind = Configuration["kind"];
            CommandLine = Configuration["CommandLine"];
            qa = Configuration["qa"];
            dn = Configuration["dn"];
            dd = Configuration["dd"];
            ProcessName = Configuration["ProcessName"];
        
            CheckRepeat();

            int.TryParse(Configuration["timeEventMin"], out eventTimeMin);
            int.TryParse(Configuration["timeEventMax"], out eventTimeMax);

            int.TryParse(Configuration["minInn"], out minInn);
            int.TryParse(Configuration["maxInn"], out maxInn);
            int.TryParse(Configuration["minFid"], out minFid);
            int.TryParse(Configuration["maxFid"], out maxFid);

            double.TryParse(Configuration["errChance"].Replace(".", ","), out errorChance);
            double.TryParse(Configuration["repeatChance"].Replace(".", ","), out repeatChance);
            Console.WriteLine(repeatChance + " " + errorChance);
            time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        }

        private void CheckCanError() // Есть ли ошибка в журнале
        {
            var r = new Random().Next(1, 100000);
            if (!onlyStatus && r < errorChance * 100000)
            {
                canError = true;
            }
        }

        private void CheckRepeat() // Есть ли потери событий
        {
            if (new Random().Next(1, 10) != 9)
            {
                isRepeat = true;
            }
            else
            {
                isRepeat = false;
            }
        }

        private void CheckRs()  //Есть ли ошибка в журнале
        {
            var r = new Random().Next(1, 100000);
            if (canError && !onlyStatus && r < 0.0001*100000)
            {
                onlyStatus = true;
            }
        }

        private string Get_Event() //Получить событие
        {
            string pattern = $"AE:{ae} n={n} cnt.s={cnts} cnt.l={cntl} kind={kind} id=\"{Guid.NewGuid()}\" qa=\"{qa}\" " +
                $"sid=\"S-0-0-00-a0726361-1600-4fae-8c45-5fff5a92c25f\" un=\"{un}\" et={time} dn=\"{dn}\" dd=\"{dd}\" rs=\"{GetRs()}\" " +
                $"inn=\"{GetInn()}\" " +
                $"fid=\"{GetFid()}\" " +
                $"sp=\"{GetSp()}\"";

            time += new Random().Next(eventTimeMin, eventTimeMax+1); // время создания события 
            RandomInc();
            CheckRs();
            ae++;
            return pattern;
        }

        public void ChangeFile() //обнулить инфу при смене файла
        {   //при изменении файла
            ae = 0;
            fileIndex++;
            CheckRepeat();

        }

        public void ChangeLog() // обнулить инфу при изменении журнала
        {
            cnts = 1;
            cntl = 0;
            fileIndex = 0;
            onlyStatus = false;
            jIndex++;
           
            CheckCanError();
        }


        private void RandomInc()   // рандомное повторение cnts (потеря события)
        {
            var r = new Random().Next(1, 1000);
            
            if (r < (int)(repeatChance*1000)) 
                countRepeat = new Random().Next(1, 4);

            if (isRepeat && countRepeat>0)  cntl++; else cnts++;

            if (countRepeat>0) countRepeat--;
        }

        #region Массивы inn, fid, sp, rs
        private string GetInn()
        {
            string inn = "";
            if (Excel.Inn.Length > 0)
            {
                for (int i = 0; i < new Random().Next(minInn, maxInn + 1); i++)
                {
                    inn += Excel.Inn[new Random().Next(0, Excel.Inn.Length-1)] + ',';
                }
                inn = inn.Substring(0, inn.Length - 1);
                return inn;
            }
            else return "000000000000";
        }

        private string GetFid()
        {
            if (Excel.Inn.Length > 0)
            {
                string fid = "";
                for (int i = 0; i < new Random().Next(minFid, maxFid + 1); i++)
                {
                    fid += Excel.Fid[new Random().Next(0, Excel.Fid.Length-1)] + ',';
                }
                fid = fid.Substring(0, fid.Length - 1);
                return fid;
            }
            else return "0000000000000000000000";
        }

        private string GetRs()
        {
            if (!onlyStatus)
                return $"{rs[new Random().Next(0, rs.Count)].Name}, {rs[new Random().Next(0, rs.Count)].Name}, {rs[new Random().Next(0, rs.Count)].Name}, {rs[new Random().Next(0, rs.Count)].Name}";
            else
                return "STATUS,STATUS,STATUS,STATUS";
        }

        private string GetSp()
        {
            if (onlyStatus)
            {
                return "ServicePoint_A";
            }
            else
            {
                string r = "";
                for (int i = 0; i < new Random().Next(1, 5); i++)
                {
                    if (i >= 3 && new Random().Next(1, 4) == 2) break;
                    r += sp[new Random().Next(0, sp.Count)].Name + ',';
                }
                r = r.Substring(0, r.Length -1);
                return r;
            }
        }

        #endregion



    }
}

using System;
using System.Collections.Generic;
using Excel = generator.Excel_Load;
namespace generator
{
    class Event
    {
        public double errorChance = 0.01;

        public double repeatChance = 0.4;

        private int eventTimeMin = 10;
        private int eventTimeMax = 100;
        private int minInn = 10;
        private int maxInn = 50;
        private int minFid = 10;
        private int maxFid = 50;

        public string GetEvent { get => Get_Event(); }
        public int GetFileIndex { get => fileIndex; }

        public string Head { get => Get_Head(); } //заголовок
        public string Tail { get => Get_Tail(); } //конец

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
        private string n = "EnviChildHost";
        private string kind = "NavigatorExecute";
        private string qa = "logicalcatalog://rolecatalog/DemoDomain/Templates/domain.query/domain";
        private string CommandLine;
        private string un;
        private string dn = "DemoDomain";
        private string dd = "Тестирование";

        private long time;

        public string FileName { get => filename; set => filename = value; }
        private string filename = "";

        private int countRepeat = 0;

        private Dictionary<int, Pattern> rs { get => Excel.Rs; }

    private Dictionary<int, Pattern> sp { get => Excel.Sp; }


        public Event(string _err, string _repeat, string machineName,string user, string _n, string _kind, string command, string _dn, string _dd, string _qa, string _processname, string timemin, string timemax,
            string mininn, string maxinn, string minfid, string maxfid)
        {
           
            MachineName = machineName;
            User = user;
            n = _n;
            kind = _kind;
            CommandLine = command;
            qa = _qa;
            dn = _dn;
            dd = _dd;
            ProcessName = _processname;
            SetUn();
            CheckRepeat();

            int.TryParse(timemin, out eventTimeMin);
            int.TryParse(timemax, out eventTimeMax);

            int.TryParse(mininn, out minInn);
            int.TryParse(maxinn, out maxInn);
            int.TryParse(minfid, out minFid);
            int.TryParse(maxfid, out maxFid);

            double err = 0.01;
            double rep = 0.1;
            double.TryParse(_err, out err);
            double.TryParse(_repeat, out rep);

            if (err >= 0 && err <= 1) errorChance = err;
            if (rep >= 0 && rep<= 1) repeatChance = rep;

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
            if (new Random().Next(1, 5) != 4)
            {
                isRepeat = true;
            }
            else
            {
                isRepeat = false;
            }
        }

        private void CheckRs() 
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

            time += new Random().Next(eventTimeMin, eventTimeMax+1);
            RandomInc();
            CheckRs();
            ae++;
            return pattern;
        }

        public void ChangeFile()
        {   //при изменении файла
            ae = 0;
            fileIndex++;
            CheckRepeat();

        }

        public void ChangeLog() // при изменении журнала
        {
            cnts = 1;
            cntl = 0;
            fileIndex = 0;
            onlyStatus = false;
            jIndex++;
            SetUn();
            CheckCanError();
        }


        private void RandomInc()   // рандомное повторение cnts
        {
            var r = new Random().Next(1, 1000);
            
            if (r < (int)(repeatChance*1000)) 
            {
                countRepeat = new Random().Next(1, 4);
               // Console.WriteLine("" + countRepeat);
            }
            if (isRepeat && countRepeat>0)
            {
                cntl++;
            }
            else
            {
                cnts++;
            }

            if (countRepeat>0)
                countRepeat--;
        }

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

        private void SetUn()
        {
            //MachineName = $"HPW{new Random().Next(10, 16)}";
            un = $"{MachineName}\\{User}";
        }

        private string Get_Tail()
        {
            return $"LI^{filename}";
        }
        private string Get_Head()
        {
            return $"LI:{filename} l.MachineName={MachineName} " +
                $"l.ProcessName={ProcessName} l.CommandLine=\"{CommandLine}\" l.Id={jIndex} " +
                $"l.StartTime=\"{DateTime.Now}\"";
        }
    }
}

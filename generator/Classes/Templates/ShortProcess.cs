using generator.Interfaces;
using generator.Interfaces.Templates;
using Microsoft.Extensions.Configuration;
using System;

namespace generator.Templates
{
    class ShortProcess: IShortProcess
    {
        public string Qa { get => RandomQaChange(); }

        public string MachineName { get => machinename; }
        public string Ssid { get { if (user_id < _excel.Ssid.Count) return _excel.Ssid[user_id].Name; else return _excel.Ssid[0].Name; } }
        public string User { get { ChangeSecondLevel(); return _excel.Users[user_id].Name; } }

        private string machinename;
        private string qa;
        private int user_id =0;
        private double QaChange;
        private double UserChance;
        private int qaCounter = 0;

        private readonly IExcel _excel;
        public ShortProcess(IConfiguration Configuration, IExcel excel)
        {
            QaChange = Converter.ConverToDouble(Configuration["ChangeQa"]);
            UserChance = Converter.ConverToDouble(Configuration["UserChance"]);
            _excel = excel;
           // Console.WriteLine("" + _excel.ErrorRead);
            user_id = new Random().Next(0, _excel.Users.Count);
            ChangeFirstLevel();
           
        }

        public void ChangeFirstLevel()
        {
            machinename = _excel.MachineName[new Random().Next(0, _excel.MachineName.Count)].Name;
            qa = _excel.Qa[new Random().Next(0, _excel.Qa.Count)].Name;
        }

        public void ChangeSecondLevel()
        {
            if (Converter.Random(UserChance))
            {
                user_id = new Random().Next(0, _excel.Users.Count);
            }
            
        }

        public string RandomQaChange()
        {
            if (qaCounter>0 || Converter.Random(QaChange))
            {
                qaCounter = new Random().Next(0, _excel.Qa.Count);
                var str = _excel.Qa[qaCounter].Name;
                if (Converter.Random(0.5))
                {
                    qaCounter = 0;
                }
                return str;
            }
            else
            {
                return qa;
            }
        }
    }
}

using generator.Static;
using Microsoft.Extensions.Configuration;
using System;

namespace generator.Templates
{
    class ShortProcess
    {
        public string Qa { get => RandomQaChange(); }

        public string MachineName { get => machinename; }
        public string Ssid { get { if (user_id < Arrays.Ssid.Count) return Arrays.Ssid[user_id].Name; else return Arrays.Ssid[0].Name; } }
        public string User { get { ChangeSecondLevel(); return Arrays.Users[user_id].Name; } }

        private string machinename;
        private string qa;
        private int user_id =0;
        private double QaChange;
        private double UserChance;
        private int qaCounter = 0;


        public ShortProcess(IConfiguration Configuration)
        {
            QaChange = Converter.ConverToDouble(Configuration["ChangeQa"]);
            UserChance = Converter.ConverToDouble(Configuration["UserChance"]);
            user_id = new Random().Next(0, Arrays.Users.Count);
            ChangeFirstLevel();
        }

        public void ChangeFirstLevel()
        {
            machinename = Arrays.MachineName[new Random().Next(0, Arrays.MachineName.Count)].Name;
            qa = Arrays.Qa[new Random().Next(0, Arrays.Qa.Count)].Name;
        }

        public void ChangeSecondLevel()
        {
            if (Converter.Random(UserChance))
            {
                user_id = new Random().Next(0, Arrays.Users.Count);
            }
            
        }

        public string RandomQaChange()
        {
            if (qaCounter>0 || Converter.Random(QaChange))
            {
                qaCounter = new Random().Next(0, Arrays.Qa.Count);
                var str = Arrays.Qa[qaCounter].Name;
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

using generator.Interfaces;
using generator.Interfaces.Templates;
using Microsoft.Extensions.Configuration;
using System;

namespace generator.Templates
{
    class ShortProcess: IShortProcess
    {
        public string Qa { get => RandomQaChange(); }

        public string MachineName { get => _machinename; }
        public string Ssid { get { if (_user_id < _excel.Ssid.Count) return _excel.Ssid[_user_id].Name; else return _excel.Ssid[0].Name; } }
        public string User { get { ChangeSecondLevel(); return _excel.Users[_user_id].Name; } }

        private string _machinename;
        private string _qa;
        private int _user_id = 0;
        private double _qaChange;
        private double _userChance;
        private int _qaCounter = 0;

        private readonly IExcel _excel;
        private readonly IConverter _converter;
        public ShortProcess(IConfiguration Configuration, IExcel excel, IConverter converter)
        {
            _converter = converter;
            _qaChange = converter.ConverToDouble(Configuration["ChangeQa"]);
            _userChance = converter.ConverToDouble(Configuration["UserChance"]);
            _excel = excel;
            // Console.WriteLine("" + _excel.ErrorRead);
            _user_id = new Random().Next(0, _excel.Users.Count);
            ChangeFirstLevel();
           
        }

        public void ChangeFirstLevel()
        {
            _machinename = _excel.MachineName[new Random().Next(0, _excel.MachineName.Count)].Name;
            _qa = _excel.Qa[new Random().Next(0, _excel.Qa.Count)].Name;
        }

        public void ChangeSecondLevel()
        {
            if (_converter.Random(_userChance))
            {
                _user_id = new Random().Next(0, _excel.Users.Count);
            }
            
        }

        public string RandomQaChange()
        {
            if (_qaCounter>0 || _converter.Random(_qaChange))
            {
                _qaCounter = new Random().Next(0, _excel.Qa.Count);
                var str = _excel.Qa[_qaCounter].Name;
                if (_converter.Random(0.5))
                {
                    _qaCounter = 0;
                }
                return str;
            }
            else
            {
                return _qa;
            }
        }
    }
}

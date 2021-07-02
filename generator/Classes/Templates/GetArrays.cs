using generator.Interfaces;
using generator.Interfaces.Templates;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace generator
{
    class GetArrays: IGetArrays //получение массивов в виде строки
    {
        public string Inn { get => GetInn(); }
        public string Fid { get => GetFid(); }
        public string Rs { get => GetRs(); }
        public string Sp { get => GetSp(); }

        private int _minRs;
        private int _maxRs;
        private int _minInn;
        private int _maxInn;
        private int _minFid;
        private int _maxFid;
        
        public bool onlyStatus { get; set; } //rs

       
        private Dictionary<int, Pattern> _rs { get => _excel.Rs; }

        private Dictionary<int, Pattern> _sp { get => _excel.Sp; }

        private readonly IExcel _excel;
        private readonly IConverter _converter;
        public GetArrays(IConfiguration Configuration, IExcel excel, IConverter converter)
        {
            _converter = converter;
            _minRs = _converter.ConverToInt(Configuration["MinRs"]);
            _maxRs = _converter.ConverToInt(Configuration["MaxRs"]);
            _minInn = _converter.ConverToInt(Configuration["MinInn"]);
            _maxInn = _converter.ConverToInt(Configuration["MaxInn"]);
            _minFid = _converter.ConverToInt(Configuration["MinFid"]);
            _maxFid = _converter.ConverToInt(Configuration["MaxFid"]);

            _excel = excel;
        }



        #region Массивы inn, fid, sp, rs
        private string GetInn()
        {
            var inn = "";
            if (_excel.Inn.Length > 0)
            {
                for (int i = 0; i < new Random().Next(_minInn, _maxInn + 1); i++)
                {
                    inn += _excel.Inn[new Random().Next(0, _excel.Inn.Length - 1)] + ',';
                }
                inn = inn.Substring(0, inn.Length - 1);
                return inn;
            }
            else return "000000000000";
        }

        private string GetFid()
        {
            if (_excel.Inn.Length > 0)
            {
                var fid = "";
                for (int i = 0; i < new Random().Next(_minFid, _maxFid + 1); i++)
                {
                    fid += _excel.Fid[new Random().Next(0, _excel.Fid.Length - 1)] + ',';
                }
                fid = fid.Substring(0, fid.Length - 1);
                return fid;
            }
            else return "0000000000000000000000";
        }

        private string GetRs()
        {
            var r = "";
            for (int i = 0; i < new Random().Next(_minRs, _maxRs + 1); i++)
            {
                    r += _rs[new Random().Next(0, _rs.Count)].Name + ',';
            }
            r = r.Substring(0, r.Length - 1);
            return r;
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
                    if (i >= 3 && _converter.Random(0.4)) break;
                    r += _sp[new Random().Next(0, _sp.Count)].Name + ',';
                }
                r = r.Substring(0, r.Length - 1);
                return r;
            }
        }

        #endregion
    }
}

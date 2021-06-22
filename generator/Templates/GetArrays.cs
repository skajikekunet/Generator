using generator.Static;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace generator
{
    class GetArrays //получение массивов в виде строки
    {
        public string Inn { get => GetInn(); }
        public string Fid { get => GetFid(); }
        public string Rs { get => GetRs(); }
        public string Sp { get => GetSp(); }

        private int minRs;
        private int maxRs;
        private int minInn;
        private int maxInn;
        private int minFid;
        private int maxFid;
        
        public bool onlyStatus = false; //rs

       
        private Dictionary<int, Pattern> rs { get => Arrays.Rs; }

        private Dictionary<int, Pattern> sp { get => Arrays.Sp; }

        public GetArrays(IConfiguration Configuration)
        {
            minRs = Converter.ConverToInt(Configuration["MinRs"]);
            maxRs = Converter.ConverToInt(Configuration["MaxRs"]);
            minInn = Converter.ConverToInt(Configuration["MinInn"]);
            maxInn = Converter.ConverToInt(Configuration["MaxInn"]);
            minFid = Converter.ConverToInt(Configuration["MinFid"]);
            maxFid = Converter.ConverToInt(Configuration["MaxFid"]);
        }



        #region Массивы inn, fid, sp, rs
        private string GetInn()
        {
            var inn = "";
            if (Arrays.Inn.Length > 0)
            {
                for (int i = 0; i < new Random().Next(minInn, maxInn + 1); i++)
                {
                    inn += Arrays.Inn[new Random().Next(0, Arrays.Inn.Length - 1)] + ',';
                }
                inn = inn.Substring(0, inn.Length - 1);
                return inn;
            }
            else return "000000000000";
        }

        private string GetFid()
        {
            if (Arrays.Inn.Length > 0)
            {
                var fid = "";
                for (int i = 0; i < new Random().Next(minFid, maxFid + 1); i++)
                {
                    fid += Arrays.Fid[new Random().Next(0, Arrays.Fid.Length - 1)] + ',';
                }
                fid = fid.Substring(0, fid.Length - 1);
                return fid;
            }
            else return "0000000000000000000000";
        }

        private string GetRs()
        {
            var r = "";
            for (int i = 0; i < new Random().Next(minRs, maxRs+1); i++)
            {
                    r += rs[new Random().Next(0, rs.Count)].Name + ',';
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
                    if (i >= 3 && Converter.Random(0.4)) break;
                    r += sp[new Random().Next(0, sp.Count)].Name + ',';
                }
                r = r.Substring(0, r.Length - 1);
                return r;
            }
        }

        #endregion
    }
}

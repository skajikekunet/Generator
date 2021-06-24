using generator.Interfaces;
using generator.Interfaces.Templates;
using generator.Templates;
using Microsoft.Extensions.Configuration;
using System;
namespace generator
{
    class Event: IEvent
    {
        private double errorChance;
        private double repeatChance;

        public string GetEvent { get => GetEventF(); }
        public int GetFileIndex { get => fileIndex; }

        public string Head { get => GetHead(); } 
        public string Tail { get => $"LI^{filename}"; } //конец файла

        private bool isRepeat = false; //есть ли повторения cnts
        private bool canError = true; //rs

        //счетчики
        private int ae = 0;
        private int cnts = 1;
        private int cntl = 0;
        private int fileIndex = 0;
        private int jIndex = 1;
        private int countRepeat = 0;
        //

        public string FileName { get => filename; set => filename = value; }
        private string filename = "";

        

        private readonly IGetArrays _arrays;
        private readonly IFileTemplate _fileTemplate;
        private readonly IShortProcess _shortProcess;

        
        public Event(IConfiguration Configuration, IExcel excel)
        {
            CheckRepeat();

            errorChance = Converter.ConverToDouble(Configuration["ErrChance"]);
            repeatChance = Converter.ConverToDouble(Configuration["RepeatChance"]);

            _arrays = new GetArrays(Configuration, excel);
            _fileTemplate = new FileTemplate(Configuration);
            _shortProcess = new ShortProcess(Configuration, excel);
        }

        private void CheckCanError() // Есть ли ошибка в журнале
        {
            if (!_arrays.onlyStatus && Converter.Random(errorChance))
            {
                canError = true;
            }
        }

        private void CheckRepeat() // Есть ли потери событий
        {
            if (Converter.Random(0.9))
                isRepeat = true;
            else
                isRepeat = false;
        }

        private void CheckRs()  //Есть ли ошибка в журнале
        {
            if (canError && !_arrays.onlyStatus && Converter.Random(0.0001))
            {
                _arrays.onlyStatus = true;
            }
        }

        private string GetEventF() //Получить событие
        {
            var pattern = $"AE:{ae} n={_fileTemplate.N} cnt.s={cnts} cnt.l={cntl} kind={_fileTemplate.Kind} id=\"{Guid.NewGuid()}\" qa=\"{_shortProcess.Qa}\" " +
                $"sid=\"{_shortProcess.Ssid}\" un=\"{_shortProcess.User}\" et={_fileTemplate.Time} dn=\"{_fileTemplate.Dn}\" dd=\"{_fileTemplate.Dd}\" rs=\"{_arrays.Rs}\" " +
                $"inn=\"{_arrays.Inn}\" " +
                $"fid=\"{_arrays.Fid}\" ";
            
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
            _arrays.onlyStatus = false;
            jIndex++;
            _shortProcess.ChangeFirstLevel();
            CheckCanError();
        }


        private void RandomInc()   // рандомное повторение cnts (потеря события)
        {
            if (Converter.Random(repeatChance)) 
                countRepeat = new Random().Next(1, 4);
            if (isRepeat && countRepeat>0)  cntl++; else cnts++;
            if (countRepeat>0) countRepeat--;
        }

        private string GetHead()
        {
            return $"LI:{filename} l.MachineName={_shortProcess.MachineName} " +
                $"l.ProcessName={_fileTemplate.ProcessName} l.CommandLine=\"{_fileTemplate.CommandLine}\" l.Id={jIndex} " +
                $"l.StartTime=\"{DateTime.Now}\"";
        }


    }
}

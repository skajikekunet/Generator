using generator.Templates;
using Microsoft.Extensions.Configuration;
using System;
namespace generator
{
    class Event
    {
        public double errorChance;
        public double repeatChance;

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

        

        private GetArrays arrays;
        private FileTemplate fileTemplate;
        private ShortProcess shortProcess;


        public Event(IConfiguration Configuration)
        {
            CheckRepeat();

            errorChance = Converter.ConverToDouble(Configuration["ErrChance"]);
            repeatChance = Converter.ConverToDouble(Configuration["RepeatChance"]);

            arrays = new GetArrays(Configuration);
            fileTemplate = new FileTemplate(Configuration);
            shortProcess = new ShortProcess(Configuration);
        }

        private void CheckCanError() // Есть ли ошибка в журнале
        {
            if (!arrays.onlyStatus && Converter.Random(errorChance))
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
            if (canError && !arrays.onlyStatus && Converter.Random(0.0001))
            {
                arrays.onlyStatus = true;
            }
        }

        private string GetEventF() //Получить событие
        {
            var pattern = $"AE:{ae} n={fileTemplate.N} cnt.s={cnts} cnt.l={cntl} kind={fileTemplate.Kind} id=\"{Guid.NewGuid()}\" qa=\"{shortProcess.Qa}\" " +
                $"sid=\"{shortProcess.Ssid}\" un=\"{shortProcess.User}\" et={fileTemplate.Time} dn=\"{fileTemplate.Dn}\" dd=\"{fileTemplate.Dd}\" rs=\"{arrays.Rs}\" " +
                $"inn=\"{arrays.Inn}\" " +
                $"fid=\"{arrays.Fid}\" ";
            
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
            arrays.onlyStatus = false;
            jIndex++;
            shortProcess.ChangeFirstLevel();
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
            return $"LI:{filename} l.MachineName={shortProcess.MachineName} " +
                $"l.ProcessName={fileTemplate.ProcessName} l.CommandLine=\"{fileTemplate.CommandLine}\" l.Id={jIndex} " +
                $"l.StartTime=\"{DateTime.Now}\"";
        }


    }
}

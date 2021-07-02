using generator.Interfaces;
using generator.Interfaces.Templates;
using generator.Templates;
using Microsoft.Extensions.Configuration;
using System;
namespace generator
{
    class Event: IEvent
    {
        private double _errorChance;
        private double _repeatChance;

        public string GetEvent { get => GetEventF(); }
        public int GetFileIndex { get => _fileIndex; }

        public string Head { get => GetHead(); } 
        public string Tail { get => $"LI^{filename}"; } //конец файла

        private bool _isRepeat = false; //есть ли повторения cnts
        private bool _canError = true; //rs

        //счетчики
        private int _ae = 0;
        private int _cnts = 1;
        private int _cntl = 0;
        private int _fileIndex = 0;
        private int _jIndex = 1;
        private int _countRepeat = 0;
        //

        public string FileName { get => filename; set => filename = value; }
        private string filename = "";

        

        private readonly IGetArrays _arrays;
        private readonly IFileTemplate _fileTemplate;
        private readonly IShortProcess _shortProcess;
        private readonly IConverter _converter;


        public Event(IConfiguration configuration, IExcel excel, IConverter converter)
        {
            _converter = converter;
            CheckRepeat();
            _errorChance = _converter.ConverToDouble(configuration["ErrChance"]);
            _repeatChance = _converter.ConverToDouble(configuration["RepeatChance"]);

            _arrays = new GetArrays(configuration, excel, converter);
            _fileTemplate = new FileTemplate(configuration, converter);
            _shortProcess = new ShortProcess(configuration, excel, converter);
        }

        private void CheckCanError() // Есть ли ошибка в журнале
        {
            if (!_arrays.onlyStatus && _converter.Random(_errorChance))
            {
                _canError = true;
            }
        }

        private void CheckRepeat() // Есть ли потери событий
        {
            if (_converter.Random(0.9))
                _isRepeat = true;
            else
                _isRepeat = false;
        }

        private void CheckRs()  //Есть ли ошибка в журнале
        {
            if (_canError && !_arrays.onlyStatus && _converter.Random(0.0001))
            {
                _arrays.onlyStatus = true;
            }
        }

        private string GetEventF() //Получить событие
        {
            var pattern = $"AE:{_ae} n={_fileTemplate.N} cnt.s={_cnts} cnt.l={_cntl} kind={_fileTemplate.Kind} id=\"{Guid.NewGuid()}\" qa=\"{_shortProcess.Qa}\" " +
                $"sid=\"{_shortProcess.Ssid}\" un=\"{_shortProcess.User}\" et={_fileTemplate.Time} dn=\"{_fileTemplate.Dn}\" dd=\"{_fileTemplate.Dd}\" rs=\"{_arrays.Rs}\" " +
                $"inn=\"{_arrays.Inn}\" " +
                $"fid=\"{_arrays.Fid}\" ";
            
            RandomInc();
            CheckRs();
            _ae++;
            return pattern;
        }

        public void ChangeFile() //обнулить инфу при смене файла
        {   //при изменении файла
            _ae = 0;
            _fileIndex++;
            CheckRepeat();
        }

        public void ChangeLog() // обнулить инфу при изменении журнала
        {
            _cnts = 1;
            _cntl = 0;
            _fileIndex = 0;
            _arrays.onlyStatus = false;
            _jIndex++;
            _shortProcess.ChangeFirstLevel();
            CheckCanError();
        }


        private void RandomInc()   // рандомное повторение cnts (потеря события)
        {
            if (_converter.Random(_repeatChance))
                _countRepeat = new Random().Next(1, 4);
            if (_isRepeat && _countRepeat > 0) _cntl++; else _cnts++;
            if (_countRepeat > 0) _countRepeat--;
        }

        private string GetHead()
        {
            return $"LI:{filename} l.MachineName={_shortProcess.MachineName} " +
                $"l.ProcessName={_fileTemplate.ProcessName} l.CommandLine=\"{_fileTemplate.CommandLine}\" l.Id={_jIndex} " +
                $"l.StartTime=\"{DateTime.Now}\"";
        }


    }
}

using System;
using System.IO;

namespace generator
{
    class GenerationFiles
    {
        private FileStream file;

        public string Path { get => path; set => path = value; }
        private string path;
        public int Get_Type(int type)
        {
            switch (type)
            {
                case 1: return type1;
                case 2: return type2;
                case 3: return type3;
                default: return -1;
            }
        }

        private int type1 = 0;
        private int type2 = 0;
        private int type3 = 0;
        public GenerationFiles(string _path)
        {
            path = _path;

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
                Console.WriteLine("Создана директория: " + path);
            }
        }

        private void Write(string txt)
        {
            file.Write(System.Text.Encoding.Default.GetBytes(txt), 0, txt.Length);
        }

        public void Generate_Type1(string info)
        {

            file = new FileStream($"{path}/type1_{type1}.txt", FileMode.Create);
            type1++;

            string mask = $"Тип файла 1, рандомное число: {info}" +
                "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                   "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                   "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                  "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                   "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                   "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd" +
                 "gdsgsgdsgsdgsgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsdgsgdsgsdgsddghsdhsd";
            Write(mask);
            Console.WriteLine($"Создан файл типа 1 \t Индекс: {type1,3} \t Размер: {file.Length}");
            file.Close();

        }

        public void Generate_Type2(string info)
        {
            file = new FileStream($"{path}/type2_{type2}.txt", FileMode.Create);
            type2++;

            string mask = $"Тип файла 2, текст: {info}";
            Write(mask);
            Console.WriteLine($"Создан файл типа 2 \t Индекс: {type2,3} \t Размер: {file.Length}");
            file.Close();
        }

        public void Generate_Type3(string info, string info2)
        {
            file = new FileStream($"{path}/type3_{type3}.txt", FileMode.Create);
            type3++;

            string mask = $"Тип файла 3, текст_число: {info}_{info2}";
            Write(mask);
            Console.WriteLine($"Создан файл типа 3 \t Индекс: {type3,3} \t Размер: {file.Length}");
            file.Close();
        }
    }
}

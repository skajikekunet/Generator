using System;
using System.IO;

namespace generator
{
    class Reader
    {
        private string path = "C:/GNIVC/test";

        private string[] files =
        {
            "210527-034457-2dacae44-7b1a-98b5-7950-47b4bf93f214-3-SLOG#14966501.slog",
            "210527-034457-ad350016-7582-37e6-d766-d1680c4a8fa9-3-SLOG#4186309.slog",
            "210527-034457-eb35409c-4394-c1e6-86b5-49d1d43c6520-3-SLOG#1458547.slog",
            "210527-034457-fd55dcaf-427a-a173-2cec-1040f26a05aa-3-SLOG#21039044.slog"
            /*"00a184dd-0252-4cb7-b669-62eef8a9f249#0.slog",
    "00a184dd-0252-4cb7-b669-62eef8a9f249#1.slog",
    "00a184dd-0252-4cb7-b669-62eef8a9f249#2.slog",
    "094ffc95-c508-4de2-9f57-1ad54091cd8b#0.slog",
    "094ffc95-c508-4de2-9f57-1ad54091cd8b#1.slog",
    "094ffc95-c508-4de2-9f57-1ad54091cd8b#2.slog",
    "1af12ee7-16b3-4998-b94b-039d5e58e92f#0.slog",
    "1af12ee7-16b3-4998-b94b-039d5e58e92f#1.slog",
    "1af12ee7-16b3-4998-b94b-039d5e58e92f#2.slog",
    "2507eee1-4a00-4732-b7f9-f69b3ff37e96#0.slog",
    "2507eee1-4a00-4732-b7f9-f69b3ff37e96#1.slog",
    "2507eee1-4a00-4732-b7f9-f69b3ff37e96#2.slog",
    "2a35a61c-b354-4b76-a814-823af693d47e#0.slog",
    "2a35a61c-b354-4b76-a814-823af693d47e#1.slog",
    "2a35a61c-b354-4b76-a814-823af693d47e#2.slog",
    "394e9126-acfa-4f66-b140-8747b794c5d9#0.slog",
    "394e9126-acfa-4f66-b140-8747b794c5d9#1.slog",
    "394e9126-acfa-4f66-b140-8747b794c5d9#2.slog",
    "3a1dd3f6-a14f-4206-97e3-5207622a8975#0.slog",
    "3a1dd3f6-a14f-4206-97e3-5207622a8975#1.slog",
    "3a1dd3f6-a14f-4206-97e3-5207622a8975#2.slog",
    "70eea75a-350a-467d-99c2-030e142728bb#0.slog",
    "70eea75a-350a-467d-99c2-030e142728bb#1.slog",
    "70eea75a-350a-467d-99c2-030e142728bb#2.slog"*/
        };
        public Reader()
        {

        }

        public void ReadCatalog()
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            Console.WriteLine("==============Список файлов==============");
            foreach (var item in dir.GetFiles())
            {
                Console.WriteLine('"' + item.Name + "\",");
            }
            Console.ReadLine();
        }

        public void ReadFiles()
        {
            foreach (string f in files)
            {
                //  FileStream file = new FileStream($"{path}/{f}", FileMode.Open);
                StreamReader file = new StreamReader($"{path}/{f}");
                StreamWriter outf = new StreamWriter($"{path}/out.txt", true);
                while (!file.EndOfStream)
                {
                    string[] mass = file.ReadLine().Split(' ');
                    if (mass.Length > 13)
                    {
                        /*string[] sp = new string[10];
                        if (mass[11].Length > 5)
                            sp = GetInn(mass[11]);
                        else continue;
                        foreach (string r in sp)
                        {
                            if (r.Length > 0)
                                outf.WriteLine(r);
                        }*/
                        outf.WriteLine(mass[5].Substring(4, mass[5].Length - 5));
                        //  outf.WriteLine("\"" + r + "\",");


                    }
                }
                file.Close();
                outf.Close();
            }
        }

     /*   public string[] GetUser(string rs)
        {
            rs = rs.Substring(4, rs.Length - 5);
            return rs.Split(',');
        }*/

        public string[] GetSp(string rs)
        {
            rs = rs.Substring(4, rs.Length - 5);
            return rs.Split(',');
        }

        public string[] GetInn(string rs)
        {
            rs = rs.Substring(4, rs.Length - 5);
            return rs.Split(',');
        }

        public string[] GetRs(string rs)
        {
            rs = rs.Substring(4, rs.Length - 5);
            return rs.Split(',');
        }

    }
}

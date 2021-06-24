using System;
using System.Collections.Generic;
using System.Text;

namespace generator.Interfaces
{
    interface IEvent
    {
       // double errorChance { get; set; }
       // double repeatChance { get; set; }

        string GetEvent { get; }
        int GetFileIndex { get; }

        string Head { get; }
        string Tail { get; }

        string FileName { get; set; }
        void ChangeFile();
        void ChangeLog();

    }
}

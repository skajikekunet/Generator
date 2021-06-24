using System;
using System.Collections.Generic;
using System.Text;

namespace generator.Interfaces.Templates
{
    interface IShortProcess
    {
        string Qa { get; }

        string MachineName { get; }
        string Ssid { get; }
        string User { get; }

        void ChangeFirstLevel();
        void ChangeSecondLevel();
        string RandomQaChange();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IOutput
    {
        public void WriteTitle(string text);
        public void WriteSubtitle(string text);
        public void WriteLine(string text);
        public void WriteLine();
        public void WriteWarning(string text);
        public void ConfirmContinue();
    }
}

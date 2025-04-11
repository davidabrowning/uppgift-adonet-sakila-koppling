using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.UI
{
    internal class Input
    {
        private readonly Output _output;
        public Input(Output output)
        {
            _output = output;
        }
        public string GetString(string prompt)
        {
            _output.WritePrompt(prompt);
            return Console.ReadLine().Trim();
        }
    }
}

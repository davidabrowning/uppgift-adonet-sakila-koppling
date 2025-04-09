using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal class Input
    {
        private readonly Output output;
        public Input(Output output)
        {
            this.output = output;
        }
        public string GetString(string prompt)
        {
            output.WritePrompt(prompt);
            return Console.ReadLine().Trim();
        }
    }
}

﻿using ADOnetSakilaKoppling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.UI
{
    internal class Keyboard : IInput
    {
        private readonly IOutput _output;
        public Keyboard(IOutput output)
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

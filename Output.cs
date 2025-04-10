using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal class Output
    {
        const ConsoleColor defaultColor = ConsoleColor.Black;
        public void Clear()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor= defaultColor;
            Console.Clear();
            
        }
        public void Write(string text, ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            Console.Write("\t" + text);
        }
        public void Write(string text) => Write(text, defaultColor);
        public void WriteLine(string text, ConsoleColor textColor) => Write(text + "\n", textColor);
        public void WriteLine(string text) => WriteLine(text, defaultColor);
        public void WriteLine() => WriteLine("");
        public void WritePrompt(string text) => Write(text + " ", ConsoleColor.DarkGreen);
        public void WriteWarning(string text) => WriteLine(text, ConsoleColor.DarkRed);
        public void WriteTitle(string text)
        {
            Clear();
            WriteLine();
            WriteLine($"=== {text} ===");
        }
        public void WriteSubtitle(string text)
        {
            WriteLine();
            WriteLine($"=== {text} ===", ConsoleColor.DarkYellow);
        }
        public void ConfirmContinue()
        {
            WriteLine();
            WriteLine("Tryck ENTER för att fortsätta.", ConsoleColor.DarkGreen);
            Console.ReadLine();
        }
    }
}

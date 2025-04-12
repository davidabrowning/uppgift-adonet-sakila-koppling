using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.UI
{
    internal class Output
    {
        const ConsoleColor DefaultColor = ConsoleColor.Black;
        public Output()
        {
            Clear(); // Necessary in order to fully fill console background color
        }
        public void Clear()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor= DefaultColor;
            Console.Clear();
            
        }
        public void Write(string text, ConsoleColor textColor)
        {
            Console.ForegroundColor = textColor;
            Console.Write("\t" + text);
        }
        public void Write(string text) => Write(text, DefaultColor);
        public void WriteLine(string text, ConsoleColor textColor) => Write(text + "\n", textColor);
        public void WriteLine(string text) => WriteLine(text, DefaultColor);
        public void WriteLine() => WriteLine("");
        public void WritePrompt(string text) => Write(text + ": ", ConsoleColor.DarkBlue);
        public void WriteTitle(string text)
        {
            WriteLine();
            WriteLine($"========= {text} =========", ConsoleColor.DarkGray);
        }
        public void WriteSubtitle(string text)
        {
            WriteLine();
            WriteLine($"========= {text} =========", ConsoleColor.DarkGray);
        }
        public void WriteWarning(string text)
        {
            WriteLine();
            WriteLine(text, ConsoleColor.DarkRed);
        }
        public void ConfirmContinue()
        {
            WriteLine();
            WriteLine("Tryck ENTER för att fortsätta.", ConsoleColor.DarkBlue);
            Console.ReadLine();
            Clear();
        }
        public void Delay() => Thread.Sleep(1);
    }
}

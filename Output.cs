using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal class Output
    {
        public void Clear()
        {
            Console.Clear();
        }
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
        public void WriteLine()
        {
            WriteLine("");
        }
        public void Write(string text)
        {
            Console.Write(text);
        }
        public void WriteTitle(string text)
        {
            Clear();
            WriteLine($"=== {text} ===");
        }
        public void WriteSubtitle(string text)
        {
            WriteLine($"\n=== {text} ===");
        }
        public void ConfirmContinue()
        {
            WriteLine("\nTryck ENTER för att fortsätta.");
            Console.ReadLine();
        }
    }
}

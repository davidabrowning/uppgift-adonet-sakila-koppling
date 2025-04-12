using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Services
{
    internal class MenuOptionClass
    {
        private static readonly int LowestId = 1;
        private static int HighestId = 0;
        public int Id { get; private set; }
        public string Title { get; private set; }
        public Action MethodToCall { get; private set; }
        public MenuOptionClass(string title, Action methodToCall)
        {
            Id = ++HighestId;
            Title = title;
            MethodToCall = methodToCall;
        }
        public void Execute()
        {
            MethodToCall();
        }
        public override string? ToString()
        {
            return $"[{Id}] {Title}";
        }
        public static bool IsValidId(int id)
        {
            return LowestId <= id && id <= HighestId;
        }
    }
}

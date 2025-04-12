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
        public string Title { get; private set; }
        public Action MethodToCall { get; private set; }
        public MenuOptionClass(string title, Action methodToCall)
        {
            Title = title;
            MethodToCall = methodToCall;
        }
    }
}

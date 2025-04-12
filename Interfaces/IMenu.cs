using ADOnetSakilaKoppling.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IMenu
    {
        void AddMenuOption(MenuOption menuOption);
        void Start();
        void Stop();
    }
}

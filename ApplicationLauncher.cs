using ADOnetSakilaKoppling.Interfaces;
using ADOnetSakilaKoppling.Menus;
using ADOnetSakilaKoppling.Repositories;
using ADOnetSakilaKoppling.Services;
using ADOnetSakilaKoppling.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal class ApplicationLauncher
    {
        public void Launch()
        {
            IOutput output = new ConsoleOutput();
            IInput input = new Keyboard(output);
            IRepository repository = new SakilaDbConnection();
            IActorService actorService = new DataService(input, output, repository);
            IMenu menu = new MainMenu(input, output);

            MenuBuilder.BuildMenuOptions(menu, actorService);

            menu.Start();
        }
    }
}

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
            Output output = new Output();
            Input input = new Input(output);
            Repository repository = new Repository();
            DataService dataService = new DataService(input, output, repository);
            Menu menu = new Menu(input, output);

            MenuBuilder.BuildMenuOptions(menu, dataService);

            menu.Start();
        }
    }
}

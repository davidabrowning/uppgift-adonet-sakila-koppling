﻿using ADOnetSakilaKoppling.Repositories;
using ADOnetSakilaKoppling.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Services
{
    internal class ProgramLauncher
    {
        public void Launch()
        {
            Output output = new Output();
            Input input = new Input(output);
            Repository repository = new Repository();
            DataService dataService = new DataService(input, output, repository);
            Menu menu = new Menu(input, output, repository);

            MenuBuilder.BuildMenuOptions(menu, dataService);

            menu.Start();
        }
    }
}

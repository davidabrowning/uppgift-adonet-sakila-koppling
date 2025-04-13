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
            IConnectionStringBuilder connectionStringBuilder = new SakilaConnectionStringBuilder();
            IDbAccess dbAccess = new SakilaDbAccess(connectionStringBuilder);
            IQueryBuilder queryBuilder = new ActorFilmQueryBuilder();
            IActorFilmRepository actorFilmRepository = new ActorFilmRepository(queryBuilder, dbAccess);
            IActorFilmService actorFilmService = new DataService(input, output, actorFilmRepository);
            IMenu menu = new MainMenu(input, output);

            MenuBuilder.BuildMenuOptions(menu, actorFilmService);

            menu.Start();
        }
    }
}

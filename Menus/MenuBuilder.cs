using ADOnetSakilaKoppling.Interfaces;
using ADOnetSakilaKoppling.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Menus
{
    internal static class MenuBuilder
    {
        public const string OptionSearchByFirstName = "Sök filmer enligt skådespelarens förnamn";
        public const string OptionSearchByLastName = "Sök filmer enligt skådespelarens efternamn";
        public const string OptionSearchByFullName = "Sök filmer enligt både förnamn och efternamn";
        public const string OptionListAllActors = "Lista ut alla skådespelare";
        public const string OptionExitProgram = "Avsluta programmet";
        public static void BuildMenuOptions(IMenu menu, IActorService actorService)
        {
            menu.AddMenuOption(new MenuOption(OptionSearchByFirstName, actorService.PrintFilmographiesByFirstName));
            menu.AddMenuOption(new MenuOption(OptionSearchByLastName, actorService.PrintFilmographiesByLastName));
            menu.AddMenuOption(new MenuOption(OptionSearchByFullName, actorService.PrintFilmographiesByFullName));
            menu.AddMenuOption(new MenuOption(OptionListAllActors, actorService.PrintAllActorNames));
            menu.AddMenuOption(new MenuOption(OptionExitProgram, menu.Stop));
        }
    }
}

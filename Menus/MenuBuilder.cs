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
        public static void BuildMenuOptions(Menu menu, DataService dataService)
        {
            menu.AddMenuOption(new MenuOption(OptionSearchByFirstName, dataService.PrintFilmographiesByFirstName));
            menu.AddMenuOption(new MenuOption(OptionSearchByLastName, dataService.PrintFilmographiesByLastName));
            menu.AddMenuOption(new MenuOption(OptionSearchByFullName, dataService.PrintFilmographiesByFullName));
            menu.AddMenuOption(new MenuOption(OptionListAllActors, dataService.PrintAllActorNames));
            menu.AddMenuOption(new MenuOption(OptionExitProgram, menu.ExitMainMenu));
        }
    }
}

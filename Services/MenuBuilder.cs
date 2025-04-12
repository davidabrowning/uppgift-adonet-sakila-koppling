using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Services
{
    internal static class MenuBuilder
    {
        public const string OptionSearchByFirstName = "Sök filmer enligt skådespelarens förnamn";
        public const string OptionSearchByLastName = "Sök filmer enligt skådespelarens efternamn";
        public const string OptionSearchByFullName = "Sök filmer enligt både förnamn och efternamn";
        public const string OptionListAllActors = "Lista ut alla skådespelare";
        public const string OptionExitProgram = "Avsluta programmet";
        public static void BuildMenuOptions(Menu menu)
        {
            menu.AddMenuOption(new MenuOption(OptionSearchByFirstName, menu.PrintFilmographiesByFirstName));
            menu.AddMenuOption(new MenuOption(OptionSearchByLastName, menu.PrintFilmographiesByLastName));
            menu.AddMenuOption(new MenuOption(OptionSearchByFullName, menu.PrintFilmographiesByFullName));
            menu.AddMenuOption(new MenuOption(OptionListAllActors, menu.PrintAllActorNames));
            menu.AddMenuOption(new MenuOption(OptionExitProgram, menu.ExitMainMenu));
        }
    }
}

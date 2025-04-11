using ADOnetSakilaKoppling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal static class MenuHelper
    {
        public const string TitleMain = "Huvudmeny";
        public const string TitleGoodbye = "Programmet avslutas";
        public const string SubtitleListAllActors = "Listar ut alla skådespelare";
        public const string OptionSearchByFirstName = "Sök filmer enligt skådespelarens förnamn";
        public const string OptionSearchByLastName = "Sök filmer enligt skådespelarens efternamn";
        public const string OptionSearchByFullName = "Sök filmer enligt både förnamn och efternamn";
        public const string OptionListAllActors = "Lista ut alla skådespelare";
        public const string OptionExitProgram = "Avsluta programmet";
        public const string MessageFilmsWith = "filmer med";
        public const string MessageGoodbye = "Tack och hej då!";
        public const string PromptChoice = "Ditt val";
        public const string PromptFirstName = "Ange förnamn";
        public const string PromptLastName = "Ange efternamn";
        public const string WarningNoActorsFound = "Inga skådespelare hittades. Försök igen.";
        public const string WarningUnexpectedInput = "Oväntad inmatning. Försök igen.";
        public static string SubtitleFilmsWithActor(Actor actor)
        {
            return $"{actor.Films.Count} {MenuHelper.MessageFilmsWith} {actor.FullName}";
        }
        public static void PrintList<T>(Output output, List<T> items, int itemsPerColumn, int columnWidth)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i > 0 && i % itemsPerColumn == 0)
                {
                    output.Delay();
                    output.WriteLine();
                }
                output.Write($"{items[i].ToString().PadRight(columnWidth)}");
            }
            output.WriteLine();
        }
        public static void PrintMainMenuOptions(Output output)
        {
            foreach (MenuOption menuOption in Enum.GetValues(typeof(MenuOption)))
                output.WriteLine((int)menuOption + ". " + GetMainMenuOption(menuOption));
        }
        private static string GetMainMenuOption(MenuOption menuOption)
        {
            switch (menuOption)
            {
                case MenuOption.SearchByFirstName:
                    return OptionSearchByFirstName;
                case MenuOption.SearchByLastName:
                    return OptionSearchByLastName;
                case MenuOption.SearchByFullName:
                    return OptionSearchByFullName;
                case MenuOption.ListAllActors:
                    return OptionListAllActors;
                case MenuOption.Exit:
                    return OptionExitProgram;
                default:
                    return WarningUnexpectedInput;
            }
        }
    }
}

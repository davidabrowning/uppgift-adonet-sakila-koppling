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
        public const string MessageFilmsWith = "filmer med";
        public const string MessageGoodbye = "Tack och hej då!";
        public const string PromptChoice = "Ditt val";
        public const string PromptFirstName = "Ange förnamn";
        public const string PromptLastName = "Ange efternamn";
        public const string WarningNoActorsFound = "Inga skådespelare hittades. Försök igen.";
        public const string WarningUnexpectedInput = "Oväntad inmatning. Försök igen.";

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
                    return "Sök filmer enligt skådespelarens förnamn";
                    case MenuOption.SearchByLastName:
                    return "Sök filmer enligt skådespelarens efternamn";
                case MenuOption.SearchByFullName:
                    return "Sök filmer enligt både förnamn och efternamn";
                case MenuOption.ListAllActors:
                    return "Lista ut alla skådespelare";
                case MenuOption.Exit:
                    return "Avsluta programmet";
                default:
                    return "Oväntad inmatning";
            }
        }
    }
}

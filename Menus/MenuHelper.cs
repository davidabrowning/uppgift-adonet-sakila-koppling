using ADOnetSakilaKoppling.Models;
using ADOnetSakilaKoppling.Services;
using ADOnetSakilaKoppling.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Menus
{
    internal static class MenuHelper
    {
        public const int ActorsPerColumn = 4;
        public const int FilmsPerColumn = 3;
        public const string TitleMain = "Huvudmeny";
        public const string TitleGoodbye = "Programmet avslutas";
        public const string SubtitleListAllActors = "Listar ut alla skådespelare";
        public const string MessageFilmsWith = "filmer med";
        public const string MessageGoodbye = "Tack och hej då!";
        public const string PromptChoice = "Ditt val";
        public const string PromptFirstName = "Ange förnamn";
        public const string PromptLastName = "Ange efternamn";
        public const string WarningMissingMenuOption = "Varning: Menyalternativ saknas.";
        public const string WarningNoActorsFound = "Inga skådespelare hittades. Försök igen.";
        public const string WarningUnexpectedInput = "Oväntad inmatning. Försök igen.";
        public static string SubtitleFilmsWithActor(Actor actor)
        {
            return $"{actor.Films.Count} {MessageFilmsWith} {actor.FullName}";
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal static class MenuHelper
    {
        public static void PrintList<T>(Output output, List<T> items, int itemsPerColumn)
        {
            int maxLength = MaxLength(items) + 1;
            for (int i = 0; i < items.Count; i++)
            {
                if (i > 0 && i % itemsPerColumn == 0)
                {
                    output.Delay();
                    output.WriteLine();
                }
                output.Write($"{items[i].ToString().PadRight(maxLength)}");
            }
            output.WriteLine();
        }
        public static int MaxLength<T>(List<T> items)
        {
            return items.Max(i => i.ToString().Length);
        }
    }
}

using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
namespace ADOnetSakilaKoppling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProgramLauncher programLauncher = new ProgramLauncher();
            programLauncher.Launch();
        }
    }
}

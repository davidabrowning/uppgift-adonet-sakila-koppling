using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
namespace ADOnetSakilaKoppling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                ClearAndShowMenuOptions();

                switch(Console.ReadLine().Trim())
                {
                    case "1":
                        Console.Write("Ange förnamn: ");
                        string firstName = Console.ReadLine().Trim();
                        PrintQueryResults($"SELECT * FROM actor WHERE first_name LIKE '{firstName}'");
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Ange efternamn: ");
                        string lastName = Console.ReadLine().Trim();
                        PrintQueryResults($"SELECT * FROM actor WHERE last_name LIKE '{lastName}'");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Ange förnamn: ");
                        string firstName2 = Console.ReadLine().Trim();
                        Console.Write("Ange efternamn: ");
                        string lastName2 = Console.ReadLine().Trim();
                        PrintQueryResults($"SELECT * FROM actor WHERE first_name LIKE '{firstName2}' AND last_name LIKE '{lastName2}'");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.Write("Listar ut alla skådespelare");
                        PrintQueryResults("SELECT * FROM actor");
                        Console.ReadLine();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Oväntad inmatning. Försök igen.");
                        break;
                }
            }
        }
        public static void ClearAndShowMenuOptions()
        {
            Console.Clear();
            Console.WriteLine("Huvudmeny");
            Console.WriteLine("1. Sök efter skådespelarens förnamn");
            Console.WriteLine("2. Sök efter skådespelarens efternamn");
            Console.WriteLine("3. Sök efter både förnamn och efternamn");
            Console.WriteLine("4. Lista ut alla skådespelare");
            Console.WriteLine("5. Avsluta programmet");
        }
        public static void PrintQueryResults(string query)
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            var command = new SqlCommand(query, connection);
            connection.Open();
            var result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Console.WriteLine($"{result[1]} {result[2]}");
                }
            }
            connection.Close();
        }
    }
}

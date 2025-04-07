using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal class Menu
    {
        private readonly Input input;
        private readonly Output output;
        private readonly Repository repository;
        public Menu(Input input, Output output, Repository repository)
        {
            this.input = input;
            this.output = output;
            this.repository = repository;
        }
        public void Start()
        {
            while (true)
            {
                ClearAndShowMenuOptions();

                switch (Console.ReadLine().Trim())
                {
                    case "1":
                        output.Write("Ange förnamn: ");
                        string firstName = Console.ReadLine().Trim();
                        repository.PrintQueryResults($"SELECT * FROM actor WHERE first_name LIKE '{firstName}'");
                        Console.ReadLine();
                        break;
                    case "2":
                        output.Write("Ange efternamn: ");
                        string lastName = Console.ReadLine().Trim();
                        repository.PrintQueryResults($"SELECT * FROM actor WHERE last_name LIKE '{lastName}'");
                        Console.ReadLine();
                        break;
                    case "3":
                        output.Write("Ange förnamn: ");
                        string firstName2 = Console.ReadLine().Trim();
                        output.Write("Ange efternamn: ");
                        string lastName2 = Console.ReadLine().Trim();
                        repository.PrintQueryResults($"SELECT * FROM actor WHERE first_name LIKE '{firstName2}' AND last_name LIKE '{lastName2}'");
                        Console.ReadLine();
                        break;
                    case "4":
                        output.Write("Listar ut alla skådespelare");
                        repository.PrintQueryResults("SELECT * FROM actor");
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
        public void ClearAndShowMenuOptions()
        {
            output.Clear();
            output.WriteLine("Huvudmeny");
            output.WriteLine("1. Sök efter skådespelarens förnamn");
            output.WriteLine("2. Sök efter skådespelarens efternamn");
            output.WriteLine("3. Sök efter både förnamn och efternamn");
            output.WriteLine("4. Lista ut alla skådespelare");
            output.WriteLine("5. Avsluta programmet");
        }
    }
}

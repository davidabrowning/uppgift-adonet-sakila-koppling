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
            ShowMainMenu();
            ShowGoodbye();
        }
        private void ShowMainMenu()
        {
            bool showMainMenu = true;
            while (showMainMenu)
            {
                ShowMainMenuOptions();
                HandleMainMenuSelection(ref showMainMenu);
            }
        }
        private void ShowMainMenuOptions()
        {
            output.WriteTitle("Huvudmeny");
            output.WriteLine("1. Sök efter skådespelarens förnamn");
            output.WriteLine("2. Sök efter skådespelarens efternamn");
            output.WriteLine("3. Sök efter både förnamn och efternamn");
            output.WriteLine("4. Lista ut alla skådespelare");
            output.WriteLine("5. Avsluta programmet");
        }
        private void HandleMainMenuSelection(ref bool showMainMenu)
        {
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    ShowMoviesByFirstName();
                    break;
                case "2":
                    ShowMoviesByLastName();
                    break;
                case "3":
                    ShowMoviesByFullName();
                    break;
                case "4":
                    ListAllActors();
                    break;
                case "5":
                    showMainMenu = false;
                    break;
                default:
                    output.WriteLine("Oväntad inmatning. Försök igen.");
                    break;
            }
        }
        private void ShowMoviesByFirstName()
        {
            output.Write("Ange förnamn: ");
            string firstName = Console.ReadLine().Trim();
            repository.PrintQueryResults($"SELECT * FROM actor WHERE first_name LIKE '{firstName}'");
            Console.ReadLine();
        }
        private void ShowMoviesByLastName()
        {
            output.Write("Ange efternamn: ");
            string lastName = Console.ReadLine().Trim();
            repository.PrintQueryResults($"SELECT * FROM actor WHERE last_name LIKE '{lastName}'");
            Console.ReadLine();
        }
        private void ShowMoviesByFullName()
        {
            output.Write("Ange förnamn: ");
            string firstName2 = Console.ReadLine().Trim();
            output.Write("Ange efternamn: ");
            string lastName2 = Console.ReadLine().Trim();
            repository.PrintQueryResults($"SELECT * FROM actor WHERE first_name LIKE '{firstName2}' AND last_name LIKE '{lastName2}'");
            Console.ReadLine();
        }
        private void ListAllActors()
        {
            output.Write("Listar ut alla skådespelare");
            repository.PrintQueryResults("SELECT * FROM actor");
            Console.ReadLine();
        }
        private void ShowGoodbye()
        {
            output.WriteLine("Tack och hej då!");
        }
    }
}

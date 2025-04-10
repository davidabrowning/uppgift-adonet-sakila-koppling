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
        private bool running;
        private readonly Input input;
        private readonly Output output;
        private readonly Repository repository;
        public Menu(Input input, Output output, Repository repository)
        {
            this.running = true;
            this.input = input;
            this.output = output;
            this.repository = repository;
        }
        public void Start()
        {
            while (running)
            {
                ShowMainMenu();
                HandleMainMenuSelection();
            }
            ShowGoodbye();
        }
        private void ShowMainMenu()
        {
            output.WriteTitle("Huvudmeny");
            output.WriteLine("1. Sök filmer enligt skådespelarens förnamn");
            output.WriteLine("2. Sök filmer enligt skådespelarens efternamn");
            output.WriteLine("3. Sök filmer enligt både förnamn och efternamn");
            output.WriteLine("4. Lista ut alla skådespelare");
            output.WriteLine("5. Avsluta programmet");
            output.WriteLine();
        }
        private void HandleMainMenuSelection()
        {
            switch (input.GetString("Ditt val:"))
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
                    running = false;
                    break;
                default:
                    output.WriteWarning("Varning: Oväntad inmatning. Försök igen.");
                    output.ConfirmContinue();
                    break;
            }
        }
        private void ShowMoviesByFirstName()
        {
            string firstName = input.GetString("Ange förnamn:");
            foreach (Actor actor in repository.GetActorsByFirstName(firstName))
                ShowActorMovies(actor);
            output.ConfirmContinue();
        }
        private void ShowMoviesByLastName()
        {
            string lastName = input.GetString("Ange efternamn:");
            foreach (Actor actor in repository.GetActorsByLastName(lastName))
                ShowActorMovies(actor);
            output.ConfirmContinue();
        }
        private void ShowMoviesByFullName()
        {
            string firstName = input.GetString("Ange förnamn:");
            string lastName = input.GetString("Ange efternamn:");
            foreach (Actor actor in repository.GetActorsByFullName(firstName, lastName))
                ShowActorMovies(actor);
            output.ConfirmContinue();
        }
        private void ShowActorMovies(Actor actor)
        {
            output.WriteSubtitle($"{actor.Films.Count} filmer med {actor.FullName}");
            int filmCounter = 0;
            foreach (Film film in actor.Films)
            {
                if (filmCounter > 0 && filmCounter % 3 == 0)
                {
                    output.WriteLine();
                    output.Delay();
                }                    
                output.Write($"{film.Title,-28}"); // Note: Max film title length is 27
                filmCounter++;
            }
            output.WriteLine();
        }
        private void ListAllActors()
        {
            output.WriteSubtitle("Listar ut alla skådespelare");
            int actorCounter = 0;
            foreach (Actor actor in repository.GetAllActors())
            {
                if (actorCounter > 0 && actorCounter % 4 == 0)
                {
                    output.WriteLine();
                    output.Delay();
                }
                output.Write($"{actor.FullName,-20}"); // Note: Max actor full name length is 19
                actorCounter++;
            }
            output.WriteLine();
            output.ConfirmContinue();
        }
        private void ShowGoodbye()
        {
            output.WriteTitle("Programmet avslutas");
            output.WriteLine("Tack och hej då!");
            output.ConfirmContinue();
            output.Clear();
        }
    }
}

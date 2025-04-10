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
        private const int ActorsPerColumn = 4;
        private const int FilmsPerColumn = 3;
        private bool _running;
        private readonly Input _input;
        private readonly Output _output;
        private readonly Repository _repository;
        public Menu(Input input, Output output, Repository repository)
        {
            _running = true;
            _input = input;
            _output = output;
            _repository = repository;
        }
        public void Start()
        {
            while (_running)
            {
                ShowMainMenu();
                HandleMainMenuSelection();
            }
            ShowGoodbye();
        }
        private void ShowMainMenu()
        {
            _output.WriteTitle("Huvudmeny");
            _output.WriteLine("1. Sök filmer enligt skådespelarens förnamn");
            _output.WriteLine("2. Sök filmer enligt skådespelarens efternamn");
            _output.WriteLine("3. Sök filmer enligt både förnamn och efternamn");
            _output.WriteLine("4. Lista ut alla skådespelare");
            _output.WriteLine("5. Avsluta programmet");
            _output.WriteLine();
        }
        private void HandleMainMenuSelection()
        {
            switch (_input.GetString("Ditt val:"))
            {
                case "1":
                    ShowFilmographiesByFirstName();
                    break;
                case "2":
                    PrintFilmographiesByLastName();
                    break;
                case "3":
                    PrintFilmographiesByFullName();
                    break;
                case "4":
                    PrintAllActorNames();
                    break;
                case "5":
                    _running = false;
                    break;
                default:
                    _output.WriteWarning("Varning: Oväntad inmatning. Försök igen.");
                    _output.ConfirmContinue();
                    break;
            }
        }
        private void ShowFilmographiesByFirstName()
        {
            string firstName = _input.GetString("Ange förnamn:");
            List<Actor> actors = _repository.GetActorsByFirstName(firstName);
            PrintFilmographies(actors);
        }
        private void PrintFilmographiesByLastName()
        {
            string lastName = _input.GetString("Ange efternamn:");
            List<Actor> actors = _repository.GetActorsByLastName(lastName);
            PrintFilmographies(actors);
        }
        private void PrintFilmographiesByFullName()
        {
            string firstName = _input.GetString("Ange förnamn:");
            string lastName = _input.GetString("Ange efternamn:");
            List<Actor> actors = _repository.GetActorsByFullName(firstName, lastName);
            PrintFilmographies(actors);
        }
        private void PrintFilmographies(List<Actor> actors)
        {
            if (actors.Count > 0)
                foreach (Actor actor in actors)
                    PrintFilmography(actor);
            else
                _output.WriteWarning("Inga skådespelare hittades. Försök igen.");
            _output.ConfirmContinue();
        }
        private void PrintFilmography(Actor actor)
        {
            _output.WriteSubtitle($"{actor.Films.Count} filmer med {actor.FullName}");
            MenuHelper.PrintList<Film>(_output, actor.Films, FilmsPerColumn);
        }
        private void PrintAllActorNames()
        {
            _output.WriteSubtitle("Listar ut alla skådespelare");
            MenuHelper.PrintList<Actor>(_output, _repository.GetAllActors(), ActorsPerColumn);
            _output.ConfirmContinue();
        }
        private void ShowGoodbye()
        {
            _output.WriteTitle("Programmet avslutas");
            _output.WriteLine("Tack och hej då!");
            _output.ConfirmContinue();
            _output.Clear();
        }
    }
}

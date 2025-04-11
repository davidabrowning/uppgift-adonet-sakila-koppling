using ADOnetSakilaKoppling.Models;
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
            _output.WriteTitle(MenuHelper.TitleMain);
            MenuHelper.PrintMainMenuOptions(_output);
            _output.WriteLine();
        }
        private void HandleMainMenuSelection()
        {
            int.TryParse(_input.GetString(MenuHelper.PromptChoice), out int menuChoice);
            if (!Enum.IsDefined(typeof(MenuOption), menuChoice))
            {
                _output.WriteWarning(MenuHelper.WarningUnexpectedInput);
                _output.ConfirmContinue();
            }
            else
                switch ((MenuOption)menuChoice)
                {
                    case MenuOption.SearchByFirstName:
                        ShowFilmographiesByFirstName();
                        break;
                    case MenuOption.SearchByLastName:
                        PrintFilmographiesByLastName();
                        break;
                    case MenuOption.SearchByFullName:
                        PrintFilmographiesByFullName();
                        break;
                    case MenuOption.ListAllActors:
                        PrintAllActorNames();
                        break;
                    case MenuOption.Exit:
                        _running = false;
                        break;
                    default:
                        _output.WriteWarning(MenuHelper.WarningUnexpectedInput);
                        _output.ConfirmContinue();
                        break;
                }
        }
        private void ShowFilmographiesByFirstName()
        {
            string firstName = _input.GetString(MenuHelper.PromptFirstName);
            List<Actor> actors = _repository.GetActorsByFirstName(firstName);
            PrintFilmographies(actors);
        }
        private void PrintFilmographiesByLastName()
        {
            string lastName = _input.GetString(MenuHelper.PromptLastName);
            List<Actor> actors = _repository.GetActorsByLastName(lastName);
            PrintFilmographies(actors);
        }
        private void PrintFilmographiesByFullName()
        {
            string firstName = _input.GetString(MenuHelper.PromptFirstName);
            string lastName = _input.GetString(MenuHelper.PromptLastName);
            List<Actor> actors = _repository.GetActorsByFullName(firstName, lastName);
            PrintFilmographies(actors);
        }
        private void PrintFilmographies(List<Actor> actors)
        {
            if (actors.Count > 0)
                foreach (Actor actor in actors)
                    PrintFilmography(actor);
            else
                _output.WriteWarning(MenuHelper.WarningNoActorsFound);
            _output.ConfirmContinue();
        }
        private void PrintFilmography(Actor actor)
        {
            _output.WriteSubtitle(MenuHelper.SubtitleFilmsWithActor(actor));
            int columnWidth = _repository.LongestFilmTitle() + 1;
            MenuHelper.PrintList<Film>(_output, actor.Films, FilmsPerColumn, columnWidth);
        }
        private void PrintAllActorNames()
        {
            _output.WriteSubtitle(MenuHelper.SubtitleListAllActors);
            int columnWidth = _repository.LongestActorName() + 1;
            MenuHelper.PrintList<Actor>(_output, _repository.GetAllActors(), ActorsPerColumn, columnWidth);
            _output.ConfirmContinue();
        }
        private void ShowGoodbye()
        {
            _output.WriteTitle(MenuHelper.TitleGoodbye);
            _output.WriteLine(MenuHelper.MessageGoodbye);
            _output.ConfirmContinue();
            _output.Clear();
        }
    }
}

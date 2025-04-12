using ADOnetSakilaKoppling.Enums;
using ADOnetSakilaKoppling.Models;
using ADOnetSakilaKoppling.Repositories;
using ADOnetSakilaKoppling.UI;
using ADOnetSakilaKoppling.Utilities;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Services
{
    internal class Menu
    {
        private const int ActorsPerColumn = 4;
        private const int FilmsPerColumn = 3;
        private bool _running;
        private readonly Input _input;
        private readonly Output _output;
        private readonly Repository _repository;
        private List<MenuOptionClass> _menuOptions;
        public Menu(Input input, Output output, Repository repository)
        {
            _running = true;
            _input = input;
            _output = output;
            _repository = repository;
            _menuOptions = MenuHelper.GetMainMenuOptions(this);
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
            foreach (MenuOptionClass menuOption in _menuOptions)
                _output.WriteLine(menuOption.ToString() ?? "Okänt menyalternativ");
            _output.WriteLine();
        }
        private void HandleMainMenuSelection()
        {
            int.TryParse(_input.GetString(MenuHelper.PromptChoice), out int menuChoice);
            if (MenuOptionClass.IsValidId(menuChoice))
                _menuOptions.Where(mo => mo.Id == menuChoice).First<MenuOptionClass>().Execute();
            else
                ShowUnexpectedInput();
        }
        public void PrintFilmographiesByFirstName()
        {
            string firstName = _input.GetString(MenuHelper.PromptFirstName);
            List<Actor> actors = _repository.GetActorsByFirstName(firstName);
            PrintFilmographies(actors);
        }
        public void PrintFilmographiesByLastName()
        {
            string lastName = _input.GetString(MenuHelper.PromptLastName);
            List<Actor> actors = _repository.GetActorsByLastName(lastName);
            PrintFilmographies(actors);
        }
        public void PrintFilmographiesByFullName()
        {
            string firstName = _input.GetString(MenuHelper.PromptFirstName);
            string lastName = _input.GetString(MenuHelper.PromptLastName);
            List<Actor> actors = _repository.GetActorsByFullName(firstName, lastName);
            PrintFilmographies(actors);
        }
        public void PrintFilmographies(List<Actor> actors)
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
            MenuHelper.PrintList(_output, actor.Films, FilmsPerColumn, columnWidth);
        }
        public void PrintAllActorNames()
        {
            _output.WriteSubtitle(MenuHelper.SubtitleListAllActors);
            int columnWidth = _repository.LongestActorName() + 1;
            MenuHelper.PrintList(_output, _repository.GetAllActors(), ActorsPerColumn, columnWidth);
            _output.ConfirmContinue();
        }
        public void ShowUnexpectedInput()
        {
            _output.WriteWarning(MenuHelper.WarningUnexpectedInput);
            _output.ConfirmContinue();
        }
        public void ExitMainMenu()
        {
            _running = false;
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

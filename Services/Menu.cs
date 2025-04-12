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
        private bool _running;
        private readonly Input _input;
        private readonly Output _output;
        private readonly Repository _repository;
        private List<MenuOption> _menuOptions = new List<MenuOption>();
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
        public void AddMenuOption(MenuOption menuOption)
        {
            _menuOptions.Add(menuOption);
        }
        private void ShowMainMenu()
        {
            _output.WriteTitle(MenuHelper.TitleMain);
            foreach (MenuOption menuOption in _menuOptions)
                _output.WriteLine(menuOption.ToString() ?? MenuHelper.WarningMissingMenuOption);
            _output.WriteLine();
        }
        private void HandleMainMenuSelection()
        {
            int.TryParse(_input.GetString(MenuHelper.PromptChoice), out int menuChoice);
            if (MenuOption.IsValidId(menuChoice))
                _menuOptions.Where(mo => mo.Id == menuChoice).First<MenuOption>().Execute();
            else
                ShowUnexpectedInput();
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

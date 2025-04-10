﻿using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal class Menu
    {
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
                    _running = false;
                    break;
                default:
                    _output.WriteWarning("Varning: Oväntad inmatning. Försök igen.");
                    _output.ConfirmContinue();
                    break;
            }
        }
        private void ShowMoviesByFirstName()
        {
            string firstName = _input.GetString("Ange förnamn:");
            foreach (Actor actor in _repository.GetActorsByFirstName(firstName))
                ShowActorMovies(actor);
            _output.ConfirmContinue();
        }
        private void ShowMoviesByLastName()
        {
            string lastName = _input.GetString("Ange efternamn:");
            foreach (Actor actor in _repository.GetActorsByLastName(lastName))
                ShowActorMovies(actor);
            _output.ConfirmContinue();
        }
        private void ShowMoviesByFullName()
        {
            string firstName = _input.GetString("Ange förnamn:");
            string lastName = _input.GetString("Ange efternamn:");
            foreach (Actor actor in _repository.GetActorsByFullName(firstName, lastName))
                ShowActorMovies(actor);
            _output.ConfirmContinue();
        }
        private void ShowActorMovies(Actor actor)
        {
            _output.WriteSubtitle($"{actor.Films.Count} filmer med {actor.FullName}");
            int filmCounter = 0;
            foreach (Film film in actor.Films)
            {
                if (filmCounter > 0 && filmCounter % 3 == 0)
                {
                    _output.Delay();
                    _output.WriteLine();
                }                    
                _output.Write($"{film.Title,-28}"); // Note: Max film title length is 27
                filmCounter++;
            }
            _output.WriteLine();
        }
        private void ListAllActors()
        {
            _output.WriteSubtitle("Listar ut alla skådespelare");
            int actorCounter = 0;
            foreach (Actor actor in _repository.GetAllActors())
            {
                if (actorCounter > 0 && actorCounter % 4 == 0)
                {
                    _output.Delay();
                    _output.WriteLine();
                }
                _output.Write($"{actor.FullName,-20}"); // Note: Max actor full name length is 19
                actorCounter++;
            }
            _output.WriteLine();
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

using ADOnetSakilaKoppling.Interfaces;
using ADOnetSakilaKoppling.Menus;
using ADOnetSakilaKoppling.Models;
using ADOnetSakilaKoppling.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Services
{
    internal class DataService : IActorService
    {
        private IInput _input;
        private IOutput _output;
        private IRepository _repository;
        public DataService(IInput input, IOutput output, IRepository repository)
        {
            _input = input;
            _output = output;
            _repository = repository;
        }
        public void PrintFilmographiesByFirstName()
        {
            string firstName = _input.GetString(MenuHelper.PromptFirstName);
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter(SakilaMapping.ActorFirstNameColumn, firstName));
            List<Actor> actors = _repository.GetSomeActors(parameters);
            PrintFilmographies(actors);
        }
        public void PrintFilmographiesByLastName()
        {
            string lastName = _input.GetString(MenuHelper.PromptLastName);
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter(SakilaMapping.ActorLastNameColumn, lastName));
            List<Actor> actors = _repository.GetSomeActors(parameters);
            PrintFilmographies(actors);
        }
        public void PrintFilmographiesByFullName()
        {
            string firstName = _input.GetString(MenuHelper.PromptFirstName);
            string lastName = _input.GetString(MenuHelper.PromptLastName);
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter(SakilaMapping.ActorFirstNameColumn, firstName));
            parameters.Add(new Parameter(SakilaMapping.ActorLastNameColumn, lastName));
            List<Actor> actors = _repository.GetSomeActors(parameters);
            PrintFilmographies(actors);
        }
        public void PrintFilmographies(List<Actor> actors)
        {
            if (actors.Count > 0)
                foreach (Actor actor in actors)
                    PrintFilmography(actor);
            else
                _output.WriteWarning(MenuHelper.WarningNoActorsFound);
        }
        private void PrintFilmography(Actor actor)
        {
            _output.WriteSubtitle(MenuHelper.SubtitleFilmsWithActor(actor));
            int columnWidth = _repository.LongestFilmTitle() + 1;
            MenuHelper.PrintList(_output, actor.Films, MenuHelper.FilmsPerColumn, columnWidth);
        }
        public void PrintAllActorNames()
        {
            _output.WriteSubtitle(MenuHelper.SubtitleListAllActors);
            int columnWidth = _repository.LongestActorName() + 1;
            MenuHelper.PrintList(_output, _repository.GetAllActors(), MenuHelper.ActorsPerColumn, columnWidth);
        }
    }
}

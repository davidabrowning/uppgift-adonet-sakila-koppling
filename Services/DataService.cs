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
    internal class DataService : IActorFilmService
    {
        private IInput _input;
        private IOutput _output;
        private IActorFilmRepository _repository;
        public DataService(IInput input, IOutput output, IActorFilmRepository actorFilmRepository)
        {
            _input = input;
            _output = output;
            _repository = actorFilmRepository;
        }
        public int LongestActorName()
        {
            return _repository.LoadActors().Max(a => a.FullName.Length);
        }
        public int LongestFilmTitle()
        {
            return _repository.LoadFilms().Max(f => f.Title.Length);
        }
        public void PrintFilmographiesByFirstName()
        {
            string firstName = _input.GetString(MenuHelper.PromptFirstName);
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter(SakilaMapping.ActorTableName, SakilaMapping.ActorFirstNameColumn, firstName));
            List<Actor> actors = _repository.LoadActors(parameters);
            PrintFilmographies(actors);
        }
        public void PrintFilmographiesByLastName()
        {
            string lastName = _input.GetString(MenuHelper.PromptLastName);
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter(SakilaMapping.ActorTableName, SakilaMapping.ActorLastNameColumn, lastName));
            List<Actor> actors = _repository.LoadActors(parameters);
            PrintFilmographies(actors);
        }
        public void PrintFilmographiesByFullName()
        {
            string firstName = _input.GetString(MenuHelper.PromptFirstName);
            string lastName = _input.GetString(MenuHelper.PromptLastName);
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter(SakilaMapping.ActorTableName, SakilaMapping.ActorFirstNameColumn, firstName));
            parameters.Add(new Parameter(SakilaMapping.ActorTableName, SakilaMapping.ActorLastNameColumn, lastName));
            List<Actor> actors = _repository.LoadActors(parameters);
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
            int columnWidth = LongestFilmTitle() + 1;
            MenuHelper.PrintList(_output, actor.Films, MenuHelper.FilmsPerColumn, columnWidth);
        }
        public void PrintAllActorNames()
        {
            _output.WriteSubtitle(MenuHelper.SubtitleListAllActors);
            int columnWidth = LongestActorName() + 1;
            MenuHelper.PrintList(_output, _repository.LoadActors(), MenuHelper.ActorsPerColumn, columnWidth);
        }
    }
}

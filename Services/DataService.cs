using ADOnetSakilaKoppling.Interfaces;
using ADOnetSakilaKoppling.Menus;
using ADOnetSakilaKoppling.Models;
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
            List<Models.SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter(ActorMapping.ActorFirstNameColumn, firstName));
            List<Actor> actors = _repository.GetActorsByFields(sqlParameters);
            PrintFilmographies(actors);
        }
        public void PrintFilmographiesByLastName()
        {
            string lastName = _input.GetString(MenuHelper.PromptLastName);
            List<Models.SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter(ActorMapping.ActorLastNameColumn, lastName));
            List<Actor> actors = _repository.GetActorsByFields(sqlParameters);
            PrintFilmographies(actors);
        }
        public void PrintFilmographiesByFullName()
        {
            string firstName = _input.GetString(MenuHelper.PromptFirstName);
            string lastName = _input.GetString(MenuHelper.PromptLastName);
            List<Models.SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter(ActorMapping.ActorFirstNameColumn, firstName));
            sqlParameters.Add(new SqlParameter(ActorMapping.ActorLastNameColumn, lastName));
            List<Actor> actors = _repository.GetActorsByFields(sqlParameters);
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

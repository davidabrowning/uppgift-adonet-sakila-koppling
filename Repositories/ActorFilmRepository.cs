using ADOnetSakilaKoppling.Interfaces;
using ADOnetSakilaKoppling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Repositories
{
    internal class ActorFilmRepository : IActorFilmRepository
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly IDbAccess _dbAccess;
        public ActorFilmRepository(IQueryBuilder queryBuilder, IDbAccess dbAccess)
        {
            _queryBuilder = queryBuilder;
            _dbAccess = dbAccess;
        }
        public List<Actor> LoadActors(List<Parameter> parameters)
        {
            List<Actor> actors = new List<Actor>();
            string actorQuery = _queryBuilder.GetActorQuery(parameters);
            foreach (string[] actorResult in _dbAccess.GetQueryResults(actorQuery, parameters))
            {
                int actorId = int.Parse(actorResult[0]);
                string actorFirstName = actorResult[1];
                string actorLastName = actorResult[2];
                actors.Add(new Actor(actorId, actorFirstName, actorLastName));
            }
            LoadFilmLists(actors);
            return actors;
        }
        public List<Actor> LoadActors()
        {
            List<Parameter> emptyParameterList = new List<Parameter>();
            return LoadActors(emptyParameterList);
        }
        public List<Film> LoadFilms(List<Parameter> parameters)
        {
            List<Film> films = new List<Film>();
            string filmQuery = _queryBuilder.GetFilmQuery(parameters);
            foreach (string[] filmResult in _dbAccess.GetQueryResults(filmQuery, parameters))
            {
                int filmId = int.Parse(filmResult[0]);
                string filmTitle = filmResult[1];
                films.Add(new Film(filmId, filmTitle));
            }
            return films;
        }
        public List<Film> LoadFilms()
        {
            List<Parameter> emptyParameterList = new List<Parameter>();
            return LoadFilms(emptyParameterList);
        }
        private void LoadFilmLists(List<Actor> actors)
        {
            foreach (Actor actor in actors)
            {
                LoadFilmList(actor);
            }
        }
        private void LoadFilmList(Actor actor)
        {
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter(
                ActorFilmMapping.ActorTableName,
                ActorFilmMapping.ActorIdColumn,
                actor.ActorId.ToString()));
            string actorFilmQuery = _queryBuilder.GetActorFilmQuery(parameters);
            List<string[]> filmResults = _dbAccess.GetQueryResults(actorFilmQuery, parameters);
            foreach (string[] filmResult in filmResults)
            {
                int filmId = int.Parse(filmResult[0]);
                string filmTitle = filmResult[1];
                actor.Add(new Film(filmId, filmTitle));
            }
        }
    }
}

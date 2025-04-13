using ADOnetSakilaKoppling.Interfaces;
using ADOnetSakilaKoppling.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Repositories
{
    internal class SakilaDbAccess : IRepository
    {
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        private readonly IQueryBuilder _queryBuilder;
        public SakilaDbAccess(IConnectionStringBuilder connectionStringBuilder, IQueryBuilder queryBuilder)
        {
            _connectionStringBuilder = connectionStringBuilder;
            _queryBuilder = queryBuilder;
        }
        private List<string[]> GetQueryResults(string query, List<Parameter> parameters)
        {
            List<string[]> results = new List<string[]>();
            using (var connection = new SqlConnection(_connectionStringBuilder.GetConnectionString()))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (Parameter parameter in parameters)
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader.Read())
                        {
                            string[] result = new string[sqlReader.FieldCount];
                            for (int i = 0; i < sqlReader.FieldCount; i++)
                                result[i] = sqlReader[i].ToString() ?? "empty";
                            results.Add(result);
                        }
                    }
                }
                connection.Close();
            }
            return results;
        }
        private List<Actor> GetActors(string actorQuery, List<Parameter> parameters)
        {
            List<Actor> actors = new List<Actor>();
            foreach (string[] actorResult in GetQueryResults(actorQuery, parameters))
            {
                int actorId = int.Parse(actorResult[0]);
                string actorFirstName = actorResult[1];
                string actorLastName = actorResult[2];
                actors.Add(new Actor(actorId, actorFirstName, actorLastName));
            }
            return actors;
        }
        private List<Film> GetFilms(string filmQuery, List<Parameter> parameters)
        {
            List<Film> films = new List<Film>();
            foreach (string[] filmResult in  GetQueryResults(filmQuery, parameters))
            {
                int filmId = int.Parse(filmResult[0]);
                string filmTitle = filmResult[1];
                films.Add(new Film(filmId, filmTitle));
            }
            return films;
        }
        private void PopulateFilmLists(List<Actor> actors)
        {
            foreach (Actor actor in actors)
            {
                PopulateFilmList(actor);
            }
        }
        private void PopulateFilmList(Actor actor)
        {
            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter(
                SakilaMapping.ActorTableName,
                SakilaMapping.ActorIdColumn,
                actor.ActorId.ToString()));
            string actorFilmQuery = _queryBuilder.GetActorFilmQuery(parameters);
            List<string[]> filmResults = GetQueryResults(actorFilmQuery, parameters);
            foreach (string[] filmResult in filmResults)
            {
                int filmId = int.Parse(filmResult[0]);
                string filmTitle = filmResult[1];
                actor.Add(new Film(filmId, filmTitle));
            }
        }
        public List<Actor> GetSomeActors(List<Parameter> parameters)
        {
            string actorQuery = _queryBuilder.GetActorQuery(parameters);
            List<Actor> actors = GetActors(actorQuery, parameters);
            PopulateFilmLists(actors);
            return actors;
        }
        public List<Actor> GetAllActors()
        {
            List<Parameter> emptyParameterList = new List<Parameter>();
            return GetSomeActors(emptyParameterList);
        }
        public List<Film> GetSomeFilms(List<Parameter> parameters)
        {
            string filmQuery = _queryBuilder.GetFilmQuery(parameters);
            List<Film> films = GetFilms(filmQuery, parameters);
            return films;
        }
        public List<Film> GetAllFilms()
        {
            List<Parameter> emptyParameterList = new List<Parameter>();
            return GetSomeFilms(emptyParameterList);
        }
        public int LongestActorName()
        {
            return GetAllActors().Max(a => a.FullName.Length);
        }
        public int LongestFilmTitle()
        {
            return GetAllFilms().Max(f => f.Title.Length);
        }
    }
}

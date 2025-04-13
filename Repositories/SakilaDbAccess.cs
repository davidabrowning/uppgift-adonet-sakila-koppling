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
        public SakilaDbAccess(IConnectionStringBuilder connectionStringBuilder)
        {
            _connectionStringBuilder = connectionStringBuilder;
        }
        private List<string[]> GetQueryResults(string query, List<Models.SqlParameter> sqlParameters)
        {
            List<string[]> results = new List<string[]>();
            using (var connection = new SqlConnection(_connectionStringBuilder.GetConnectionString()))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (Models.SqlParameter sqlParameter in sqlParameters)
                        command.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
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
        private List<Actor> GetActors(string actorQuery, List<Models.SqlParameter> sqlParameters)
        {
            List<Actor> actors = new List<Actor>();
            foreach (string[] actorResult in GetQueryResults(actorQuery, sqlParameters))
            {
                int actorId = int.Parse(actorResult[0]);
                string actorFirstName = actorResult[1];
                string actorLastName = actorResult[2];
                actors.Add(new Actor(actorId, actorFirstName, actorLastName));
            }
            return actors;
        }
        private List<Film> GetFilms(string filmQuery, List<Models.SqlParameter> sqlParameters)
        {
            List<Film> films = new List<Film>();
            foreach (string[] filmResult in  GetQueryResults(filmQuery, sqlParameters))
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
                string filmQuery = 
                    $"SELECT film.film_id, film.title " +
                    $"FROM film " +
                    $"INNER JOIN film_actor ON film_actor.film_id = film.film_id " +
                    $"INNER JOIN actor ON actor.actor_id = film_actor.actor_id " +
                    $"WHERE actor.actor_id = @actor_id " 
                    +"ORDER BY film.title ASC";
                List<Models.SqlParameter> sqlParameters = new List<Models.SqlParameter>();
                sqlParameters.Add(new Models.SqlParameter(ActorMapping.ActorIdColumn, actor.ActorId.ToString()));
                List<string[]> filmResults = GetQueryResults(filmQuery, sqlParameters);
                foreach (string[] filmResult in filmResults)
                {
                    int filmId = int.Parse(filmResult[0]);
                    string filmTitle = filmResult[1];
                    actor.Add(new Film(filmId, filmTitle));
                }
            }
        }
        public List<Actor> GetActorsByFields(List<Models.SqlParameter> sqlParameters)
        {
            string actorQuery =
                QueryBuilder.GetSelectClause() +
                QueryBuilder.GetFromClause() +
                QueryBuilder.GetWhereClause(sqlParameters) +
                QueryBuilder.GetOrderByClause();
            List<Actor> actors = GetActors(actorQuery, sqlParameters);
            PopulateFilmLists(actors);
            return actors;
        }
        public List<Actor> GetAllActors()
        {
            string actorQuery =
                $"SELECT actor_id, first_name, last_name " +
                $"FROM actor " +
                $"ORDER BY last_name ASC, first_name ASC";
            List<Models.SqlParameter> emptySqlParameterList = new List<Models.SqlParameter>();
            return GetActors(actorQuery, emptySqlParameterList);
        }
        public List<Film> GetAllFilms()
        {
            string filmQuery =
                $"SELECT film_id, title " +
                $"FROM film " +
                $"ORDER BY title ASC";
            List<Models.SqlParameter> emptySqlParameterList = new List<Models.SqlParameter>();
            return GetFilms(filmQuery, emptySqlParameterList);
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

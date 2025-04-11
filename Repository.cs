using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal class Repository
    {
        private List<string[]> GetQueryResults(string query, List<string[]> parameters)
        {
            List<string[]> results = new List<string[]>();
            string databaseConnectionString = GetConnectionString();

            using (var connection = new SqlConnection(databaseConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (string[] parameter in parameters)
                        command.Parameters.AddWithValue(parameter[0], parameter[1]);
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
        private string GetConnectionString()
        {
            string appsettings = File.ReadAllText("Appsettings.json");
            JsonDocument appsettingsJson = JsonDocument.Parse(appsettings);
            string connectionString =
                appsettingsJson
                .RootElement
                .GetProperty("ConnectionStrings")
                .GetProperty("Sakila")
                .ToString();
            return connectionString;
        }
        private List<Actor> GetActors(string actorQuery, List<string[]> parameters)
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
        private List<Film> GetFilms(string filmQuery, List<string[]> parameters)
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
                string filmQuery = 
                    $"SELECT film.film_id, film.title " +
                    $"FROM film " +
                    $"INNER JOIN film_actor ON film_actor.film_id = film.film_id " +
                    $"INNER JOIN actor ON actor.actor_id = film_actor.actor_id " +
                    $"WHERE actor.actor_id = @actorId";
                List<string[]> parameters = new List<string[]>();
                parameters.Add(["@actorId", actor.ActorId.ToString()]);
                List<string[]> filmResults = GetQueryResults(filmQuery, parameters);
                foreach (string[] filmResult in filmResults)
                {
                    int filmId = int.Parse(filmResult[0]);
                    string filmTitle = filmResult[1];
                    actor.Add(new Film(filmId, filmTitle));
                }
            }
        }
        public List<Actor> GetActorsByFirstName(string firstName)
        {
            string actorQuery =
                $"SELECT actor_id, first_name, last_name " +
                $"FROM actor " +
                $"WHERE first_name = @firstName " +
                $"ORDER BY first_name ASC, last_name ASC";
            List<string[]> parameters = new List<string[]>();
            parameters.Add(["@firstName", firstName]);
            List<Actor> actors = GetActors(actorQuery, parameters);
            PopulateFilmLists(actors);
            return actors;
        }
        public List<Actor> GetActorsByLastName(string lastName)
        {
            string actorQuery =
                $"SELECT actor_id, first_name, last_name " +
                $"FROM actor " +
                $"WHERE last_name = @lastName " +
                $"ORDER BY first_name ASC, last_name ASC";
            List<string[]> parameters = new List<string[]>();
            parameters.Add(["@lastName", lastName]);
            List<Actor> actors = GetActors(actorQuery, parameters);
            PopulateFilmLists(actors);
            return actors;
        }
        public List<Actor> GetActorsByFullName(string firstName, string lastName)
        {
            string actorQuery =
                $"SELECT actor_id, first_name, last_name " +
                $"FROM actor " +
                $"WHERE first_name = @firstName " +
                $"AND last_name = @lastName " +
                $"ORDER BY first_name ASC, last_name ASC";
            List<string[]> parameters = new List<string[]>();
            parameters.Add(["@firstName", firstName]);
            parameters.Add(["@lastName", lastName]);
            List<Actor> actors = GetActors(actorQuery, parameters);
            PopulateFilmLists(actors);
            return actors;
        }
        public List<Actor> GetAllActors()
        {
            string actorQuery =
                $"SELECT actor_id, first_name, last_name " +
                $"FROM actor " +
                $"ORDER BY first_name ASC, last_name ASC";
            List<string[]> emptyParameterList = new List<string[]>();
            return GetActors(actorQuery, emptyParameterList);
        }
        public List<Film> GetAllFilms()
        {
            string filmQuery =
                $"SELECT film_id, title " +
                $"FROM film " +
                $"ORDER BY title ASC";
            List<string[]> emptyParameterList = new List<string[]>();
            return GetFilms(filmQuery, emptyParameterList);
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

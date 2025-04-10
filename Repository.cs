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

            string appsettings = File.ReadAllText("Appsettings.json");
            JsonDocument appsettingsJson = JsonDocument.Parse(appsettings);
            string connectionString = 
                appsettingsJson
                .RootElement
                .GetProperty("ConnectionStrings")
                .GetProperty("Sakila")
                .ToString();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (string[] parameter in parameters)
                        command.Parameters.AddWithValue(parameter[0], parameter[1]);
                    using (var result = command.ExecuteReader())
                    {
                        if (result.HasRows)
                        {
                            while (result.Read())
                            {
                                string[] newResult = new string[result.FieldCount];
                                for (int i = 0; i < result.FieldCount; i++)
                                {
                                    newResult[i] = result[i].ToString();
                                }
                                results.Add(newResult);
                            }
                        }
                    }
                }
                connection.Close();
            }
            return results;
        }
        private List<Actor> GetActors(string actorQuery, List<string[]> parameters)
        {
            List<Actor> actors = new List<Actor>();
            foreach (string[] actorResult in GetQueryResults(actorQuery, parameters))
            {
                if (int.TryParse(actorResult[0], out int actorId))
                    actors.Add(new Actor(actorId, actorResult[1], actorResult[2]));
            }
            return actors;
        }
        private void PopulateFilmLists(List<Actor> actors)
        {
            foreach (Actor actor in actors)
            {
                string filmQuery = 
                    $"SELECT * FROM film " +
                    $"INNER JOIN film_actor ON film_actor.film_id = film.film_id " +
                    $"INNER JOIN actor ON actor.actor_id = film_actor.actor_id " +
                    $"WHERE actor.actor_id = @actorId";
                List<string[]> parameters = new List<string[]>();
                parameters.Add(["@actorId", actor.ActorId.ToString()]);
                List<string[]> filmResults = GetQueryResults(filmQuery, parameters);
                foreach (string[] filmResult in filmResults)
                {
                    int.TryParse(filmResult[0], out int filmId);
                    actor.Add(new Film(filmId, filmResult[1]));
                }
            }
        }
        public List<Actor> GetActorsByFirstName(string firstName)
        {
            string actorQuery =
                $"SELECT * FROM actor " +
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
            string actorQuery = $"SELECT * FROM actor " +
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
            string actorQuery = $"SELECT * FROM actor " +
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
            string actorQuery = $"SELECT * FROM actor " +
                $"ORDER BY first_name ASC, last_name ASC";
            List<string[]> emptyParameterList = new List<string[]>();
            return GetActors(actorQuery, emptyParameterList);
        }
    }
}

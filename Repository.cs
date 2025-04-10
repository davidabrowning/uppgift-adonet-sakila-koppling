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
        public List<string[]> GetQueryResultsWithoutParameters(string query)
        {
            List<string[]> emptyParameterList = new List<string[]>();
            return GetQueryResults(query, emptyParameterList);
        }
        public List<string[]> GetQueryResults(string query, List<string[]> parameters)
        {
            List<string[]> results = new List<string[]>();

            string appsettings = File.ReadAllText("Appsettings.json");
            JsonDocument appsettingsJson = JsonDocument.Parse(appsettings);
            string connectionString = appsettingsJson
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
        public List<Actor> GetActors(string actorQuery)
        {
            List<Actor> actors = new List<Actor>();
            foreach (string[] actorResult in GetQueryResultsWithoutParameters(actorQuery))
            {
                int.TryParse(actorResult[0], out int actorId);
                actors.Add(new Actor(actorId, actorResult[1], actorResult[2]));
            }                
            return actors;
        }
        public List<Actor> GetActorsWithParameters(string actorQuery, List<string[]> parameters)
        {
            List<Actor> actors = new List<Actor>();
            foreach (string[] actorResult in GetQueryResults(actorQuery, parameters))
            {
                int.TryParse(actorResult[0], out int actorId);
                actors.Add(new Actor(actorId, actorResult[1], actorResult[2]));
            }
            return actors;
        }
        public void PopulateFilmLists(List<Actor> actors)
        {
            foreach (Actor actor in actors)
            {
                string filmQuery = 
                    $"SELECT * FROM film " +
                    $"INNER JOIN film_actor ON film_actor.film_id = film.film_id " +
                    $"INNER JOIN actor ON actor.actor_id = film_actor.actor_id " +
                    $"WHERE actor.actor_id = {actor.ActorId}";
                List<string[]> filmResults = GetQueryResultsWithoutParameters(filmQuery);
                foreach (string[] filmResult in filmResults)
                {
                    int.TryParse(filmResult[0], out int filmId);
                    actor.Add(new Film(filmId, filmResult[1]));
                }
            }
        }
        public List<Actor> GetActorsAndFilmsByActorFirstNameWithParameters(string firstName)
        {
            List<string[]> parameters = new List<string[]>();
            parameters.Add(["@firstName", firstName]);
            List<Actor> actors = GetActorsWithParameters(
                $"SELECT * FROM actor " +
                $"WHERE first_name = @firstName " +
                $"ORDER BY first_name ASC, last_name ASC", parameters);
            PopulateFilmLists(actors);
            return actors;
        }
        public List<Actor> GetActorsAndFilmsByActorLastNameWithParameters(string lastName)
        {
            List<string[]> parameters = new List<string[]>();
            parameters.Add(["@lastName", lastName]);
            List<Actor> actors = GetActorsWithParameters(
                $"SELECT * FROM actor " +
                $"WHERE last_name = @lastName " +
                $"ORDER BY first_name ASC, last_name ASC", parameters);
            PopulateFilmLists(actors);
            return actors;
        }
        public List<Actor> GetActorsAndFilmsByActorFullName(string firstName, string lastName)
        {
            List<string[]> parameters = new List<string[]>();
            parameters.Add(["@firstName", firstName]);
            parameters.Add(["@lastName", lastName]);
            List<Actor> actors = GetActorsWithParameters(
                $"SELECT * FROM actor " +
                $"WHERE first_name = @firstName " +
                $"AND last_name = @lastName " +
                $"ORDER BY first_name ASC, last_name ASC", parameters);
            PopulateFilmLists(actors);
            return actors;
        }
        public List<Actor> GetAllActors()
        {
            return GetActors(
                $"SELECT * FROM actor " +
                $"ORDER BY first_name ASC, last_name ASC");
        }
    }
}

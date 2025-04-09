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
        private readonly Output output;
        public Repository(Output output)
        {
            this.output = output;
        }
        public List<string[]> GetQueryResults(string query)
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
            foreach (string[] actorResult in GetQueryResults(actorQuery))
                actors.Add(new Actor(actorResult[1], actorResult[2]));
            return actors;
        }
        public void ShowActorsAndTheirFilms(string actorQuery)
        {
            foreach (string[] actorResult in GetQueryResults(actorQuery))
            {
                Actor actor = new Actor(actorResult[1], actorResult[2]);
                int.TryParse(actorResult[0], out int actorId);
                string filmQuery = "SELECT * FROM film " +
                    "INNER JOIN film_actor ON film_actor.film_id = film.film_id " +
                    "INNER JOIN actor ON actor.actor_id = film_actor.actor_id " +
                    "WHERE actor.actor_id = " + actorId;
                List<string[]> filmResults = GetQueryResults(filmQuery);

                output.WriteSubtitle($"{filmResults.Count} filmer med {actor.FullName}");
                int filmCounter = 0;
                foreach (string[] filmResult in filmResults)
                {
                    Film film = new Film(filmResult[1]);
                    if (filmCounter > 0 &&  filmCounter % 3 == 0)
                        output.WriteLine();
                    // Note: Max filmResult name length is 27
                    output.Write($"{film.Title, -28}");
                    actor.Add(film);
                    filmCounter++;
                }
                output.WriteLine();
            }
        }
        public void ShowMoviesByActorFirstName(string firstName)
        {
            ShowActorsAndTheirFilms($"SELECT * FROM actor WHERE first_name LIKE '{firstName}'");
        }
        public void ShowMoviesByActorLastName(string lastName)
        {
            ShowActorsAndTheirFilms($"SELECT * FROM actor WHERE last_name LIKE '{lastName}'");
        }
        public void ShowMoviesByActorFullName(string firstName, string lastName)
        {
            ShowActorsAndTheirFilms($"SELECT * FROM actor WHERE first_name LIKE '{firstName}' AND last_name LIKE '{lastName}'");
        }
        public List<Actor> GetAllActors()
        {
            return GetActors($"SELECT * FROM actor");
        }
    }
}

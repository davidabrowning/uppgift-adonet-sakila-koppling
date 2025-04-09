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
        public void ShowActors(string actorQuery)
        {
            int actorCounter = 0;
            foreach (string[] actor in GetQueryResults(actorQuery))
            {
                if (actorCounter > 0 && actorCounter % 4 == 0)
                    output.WriteLine();
                // Note: Max actor full name length is 19
                string actorFullName = $"{actor[1]} {actor[2]}";
                output.Write($"{actorFullName, -20}");
                actorCounter++;
            }
            output.WriteLine();
        }
        public void ShowActorsAndTheirFilms(string actorQuery)
        {
            foreach (string[] actor in GetQueryResults(actorQuery))
            {
                int.TryParse(actor[0], out int actorId);
                string filmQuery = "SELECT * FROM film " +
                    "INNER JOIN film_actor ON film_actor.film_id = film.film_id " +
                    "INNER JOIN actor ON actor.actor_id = film_actor.actor_id " +
                    "WHERE actor.actor_id = " + actorId;
                List<string[]> filmList = GetQueryResults(filmQuery);

                output.WriteSubtitle($"{filmList.Count} filmer med {actor[1]} {actor[2]}");
                int filmCounter = 0;
                foreach (string[] film in filmList)
                {
                    if (filmCounter > 0 &&  filmCounter % 3 == 0)
                        output.WriteLine();
                    // Note: Max film name length is 27
                    string filmTitle = film[1];
                    output.Write($"{filmTitle, -28}");
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
        public void ShowActorList()
        {
            ShowActors($"SELECT * FROM actor");
        }
    }
}

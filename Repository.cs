using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            var command = new SqlCommand(query, connection);
            connection.Open();
            var result = command.ExecuteReader();
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
            connection.Close();

            return results;
        }
        public void ShowActorsAndTheirFilms(string actorQuery)
        {
            foreach (string[] actor in GetQueryResults(actorQuery))
            {
                output.WriteSubtitle($"Films with {actor[0]} {actor[1]} {actor[2]}");
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
            ShowActorsAndTheirFilms($"SELECT * FROM actor");
        }
    }
}

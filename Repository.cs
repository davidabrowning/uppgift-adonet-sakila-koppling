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
        public void PrintQueryResults(string query)
        {
            var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sakila;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            var command = new SqlCommand(query, connection);
            connection.Open();
            var result = command.ExecuteReader();
            if (result.HasRows)
            {
                int resultCounter = 1;
                while (result.Read())
                {
                    output.WriteLine($"{resultCounter++}. {result[1]} {result[2]}");
                }
            }
            connection.Close();
        }
    }
}

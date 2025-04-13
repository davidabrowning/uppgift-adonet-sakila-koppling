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

namespace ADOnetSakilaKoppling.Database
{
    internal class DbAccess : IDbAccess
    {
        private readonly IConnectionStringBuilder _connectionStringBuilder;
        public DbAccess(IConnectionStringBuilder connectionStringBuilder)
        {
            _connectionStringBuilder = connectionStringBuilder;
        }
        public List<string[]> GetQueryResults(string query, List<Parameter> parameters)
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
    }
}

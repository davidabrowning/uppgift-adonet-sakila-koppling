using ADOnetSakilaKoppling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Repositories
{
    internal class SakilaConnectionStringBuilder : IConnectionStringBuilder
    {
        public string GetConnectionString()
        {
            string appsettings = File.ReadAllText("Configurations/Appsettings.json");
            JsonDocument appsettingsJson = JsonDocument.Parse(appsettings);
            string connectionString =
                appsettingsJson
                .RootElement
                .GetProperty("ConnectionStrings")
                .GetProperty("Sakila")
                .ToString();
            return connectionString;
        }
    }
}

using ADOnetSakilaKoppling.Database;
using ADOnetSakilaKoppling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IDbAccess
    {
        public List<string[]> GetQueryResults(string query, List<Parameter> parameters);
    }
}

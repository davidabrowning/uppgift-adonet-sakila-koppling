using ADOnetSakilaKoppling.Models;
using ADOnetSakilaKoppling.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IRepository
    {
        public List<string[]> GetQueryResults(string query, List<Parameter> parameters);
    }
}

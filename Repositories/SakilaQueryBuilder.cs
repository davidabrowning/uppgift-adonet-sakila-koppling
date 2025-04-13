using ADOnetSakilaKoppling.Interfaces;
using ADOnetSakilaKoppling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Repositories
{
    internal class SakilaQueryBuilder : IQueryBuilder
    {
        public string GetSelectClause()
        {
            return $"SELECT {ActorMapping.ActorIdColumn}, " +
                $"{ActorMapping.ActorFirstNameColumn}, " +
                $"{ActorMapping.ActorLastNameColumn}";
        }
        public string GetFromClause()
        {
            return $" FROM {ActorMapping.ActorTableName}";
        }
        public string GetWhereClause(List<Parameter> parameters)
        {
            string whereClause = " WHERE 1 = 1";
            foreach (Parameter parameter in parameters)
            {
                whereClause += $" AND {parameter.ColumnName} = @{parameter.ColumnName}";
            }
            return whereClause;
        }
        public string GetOrderByClause()
        {
            return $" ORDER BY {ActorMapping.ActorLastNameColumn} ASC, {ActorMapping.ActorFirstNameColumn} ASC";
        }
    }
}

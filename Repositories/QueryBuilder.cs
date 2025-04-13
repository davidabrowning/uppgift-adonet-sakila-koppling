using ADOnetSakilaKoppling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Repositories
{
    internal static class QueryBuilder
    {
        public static string GetSelectClause()
        {
            return $"SELECT {ActorMapping.ActorIdColumn}, " +
                $"{ActorMapping.ActorFirstNameColumn}, " +
                $"{ActorMapping.ActorLastNameColumn}";
        }
        public static string GetFromClause()
        {
            return $" FROM {ActorMapping.ActorTableName}";
        }
        public static string GetWhereClause(List<SqlParameter> sqlParameters)
        {
            string whereClause = " WHERE 1 = 1";
            foreach (SqlParameter sqlParameter in sqlParameters)
            {
                whereClause += $" AND {sqlParameter.ColumnName} = @{sqlParameter.ColumnName}";
            }
            return whereClause;
        }
        public static string GetOrderByClause()
        {
            return $" ORDER BY {ActorMapping.ActorLastNameColumn} ASC, {ActorMapping.ActorFirstNameColumn} ASC";
        }
    }
}

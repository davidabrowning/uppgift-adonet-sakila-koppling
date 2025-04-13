using ADOnetSakilaKoppling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Repositories
{
    internal class SakilaQueryBuilder : IQueryBuilder
    {
        public string GetActorQuery(List<Parameter> parameters)
        {
            return 
                GetActorSelectClause() +
                GetActorFromClause() +
                GetActorWhereClause(parameters) +
                GetActorOrderByClause();
        }
        public string GetAllActorsQuery()
        {
            return GetActorQuery(new List<Parameter>());
        }
        private string GetActorSelectClause()
        {
            return $"SELECT " +
                $"{SakilaMapping.ActorIdColumn}, " +
                $"{SakilaMapping.ActorFirstNameColumn}, " +
                $"{SakilaMapping.ActorLastNameColumn}";
        }
        private string GetActorFromClause()
        {
            return $" FROM {SakilaMapping.ActorTableName}";
        }
        private string GetActorWhereClause(List<Parameter> parameters)
        {
            string whereClause = " WHERE 1 = 1";
            foreach (Parameter parameter in parameters)
            {
                whereClause += $" AND {parameter.ColumnName} = {parameter.ParameterName}";
            }
            return whereClause;
        }
        private string GetActorOrderByClause()
        {
            return $" ORDER BY {SakilaMapping.ActorLastNameColumn} ASC, {SakilaMapping.ActorFirstNameColumn} ASC";
        }
    }
}

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
        private string GetActorSelectClause()
        {
            return 
                $"SELECT" +
                $" {SakilaMapping.ActorIdColumn}," +
                $" {SakilaMapping.ActorFirstNameColumn}," +
                $" {SakilaMapping.ActorLastNameColumn}";
        }
        private string GetActorFromClause()
        {
            return 
                $" FROM {SakilaMapping.ActorTableName}";
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
            return 
                $" ORDER BY" +
                $" {SakilaMapping.ActorLastNameColumn} ASC," +
                $" {SakilaMapping.ActorFirstNameColumn} ASC";
        }
        public string GetFilmQuery(List<Parameter> parameters)
        {
            return
                GetFilmSelectClause() +
                GetFilmFromClause() +
                GetFilmWhereClause(parameters) +
                GetFilmOrderByClause();
        }
        private string GetFilmSelectClause()
        {
            return 
                $"SELECT" +
                $" {SakilaMapping.FilmIdColumn}," +
                $" {SakilaMapping.FilmTitleColumn}";
        }
        private string GetFilmFromClause()
        {
            return $" FROM {SakilaMapping.FilmTableName}";
        }
        private string GetFilmWhereClause(List<Parameter> parameters)
        {
            string whereClause = " WHERE 1 = 1";
            foreach (Parameter parameter in parameters)
            {
                whereClause += $" AND {parameter.ColumnName} = {parameter.ParameterName}";
            }
            return whereClause;
        }
        private string GetFilmOrderByClause()
        {
            return
                $" ORDER BY" +
                $" {SakilaMapping.FilmTitleColumn} ASC";
        }
    }
}

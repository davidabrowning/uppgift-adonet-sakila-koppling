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
        private string GetWhereClause(List<Parameter> parameters)
        {
            string whereClause = " WHERE 1 = 1";
            foreach (Parameter parameter in parameters)
            {
                whereClause += $" AND {parameter.TableName}.{parameter.ColumnName} = {parameter.ParameterName}";
            }
            return whereClause;
        }
        public string GetActorQuery(List<Parameter> parameters)
        {
            return 
                GetActorSelectClause() +
                GetActorFromClause() +
                GetWhereClause(parameters) +
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
                GetWhereClause(parameters) +
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
        private string GetFilmOrderByClause()
        {
            return
                $" ORDER BY" +
                $" {SakilaMapping.FilmTitleColumn} ASC";
        }
        public string GetActorFilmQuery(List<Parameter> parameters)
        {
            return
                GetActorFilmSelectClause() +
                GetActorFilmFromClause() +
                GetWhereClause(parameters) +
                GetActorFilmOrderByClause();
        }
        private string GetActorFilmSelectClause()
        {
            return $"SELECT" +
                $" {SakilaMapping.FilmTableName}.{SakilaMapping.FilmIdColumn}," +
                $" {SakilaMapping.FilmTableName}.{SakilaMapping.FilmTitleColumn}";
        }
        private string GetActorFilmFromClause()
        {
            return $" FROM {SakilaMapping.FilmTableName}" +
                $" INNER JOIN {SakilaMapping.ActorFilmTableName}" +
                $" ON {SakilaMapping.ActorFilmTableName}.{SakilaMapping.ActorFilmFilmIdColumn}" +
                $" = {SakilaMapping.FilmTableName}.{SakilaMapping.FilmIdColumn}" +
                $" INNER JOIN {SakilaMapping.ActorTableName}" +
                $" ON {SakilaMapping.ActorTableName}.{SakilaMapping.ActorIdColumn}" +
                $" = {SakilaMapping.ActorFilmTableName}.{SakilaMapping.ActorFilmActorIdColumn}";
        }
        private string GetActorFilmOrderByClause()
        {
            return $" ORDER BY {SakilaMapping.FilmTitleColumn} ASC";
        }
    }
}

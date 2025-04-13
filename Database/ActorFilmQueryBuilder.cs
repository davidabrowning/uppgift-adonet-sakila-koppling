using ADOnetSakilaKoppling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Database
{
    internal class ActorFilmQueryBuilder : IQueryBuilder
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
                $" {ActorFilmMapping.ActorIdColumn}," +
                $" {ActorFilmMapping.ActorFirstNameColumn}," +
                $" {ActorFilmMapping.ActorLastNameColumn}";
        }
        private string GetActorFromClause()
        {
            return 
                $" FROM {ActorFilmMapping.ActorTableName}";
        }
        private string GetActorOrderByClause()
        {
            return 
                $" ORDER BY" +
                $" {ActorFilmMapping.ActorLastNameColumn} ASC," +
                $" {ActorFilmMapping.ActorFirstNameColumn} ASC";
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
                $" {ActorFilmMapping.FilmIdColumn}," +
                $" {ActorFilmMapping.FilmTitleColumn}";
        }
        private string GetFilmFromClause()
        {
            return $" FROM {ActorFilmMapping.FilmTableName}";
        }
        private string GetFilmOrderByClause()
        {
            return
                $" ORDER BY" +
                $" {ActorFilmMapping.FilmTitleColumn} ASC";
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
                $" {ActorFilmMapping.FilmTableName}.{ActorFilmMapping.FilmIdColumn}," +
                $" {ActorFilmMapping.FilmTableName}.{ActorFilmMapping.FilmTitleColumn}";
        }
        private string GetActorFilmFromClause()
        {
            return $" FROM {ActorFilmMapping.FilmTableName}" +
                $" INNER JOIN {ActorFilmMapping.ActorFilmTableName}" +
                $" ON {ActorFilmMapping.ActorFilmTableName}.{ActorFilmMapping.ActorFilmFilmIdColumn}" +
                $" = {ActorFilmMapping.FilmTableName}.{ActorFilmMapping.FilmIdColumn}" +
                $" INNER JOIN {ActorFilmMapping.ActorTableName}" +
                $" ON {ActorFilmMapping.ActorTableName}.{ActorFilmMapping.ActorIdColumn}" +
                $" = {ActorFilmMapping.ActorFilmTableName}.{ActorFilmMapping.ActorFilmActorIdColumn}";
        }
        private string GetActorFilmOrderByClause()
        {
            return $" ORDER BY {ActorFilmMapping.FilmTitleColumn} ASC";
        }
    }
}

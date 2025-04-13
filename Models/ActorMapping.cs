using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Models
{
    internal class ActorMapping
    {
        public const string ActorTableName = "actor";
        public const string ActorIdColumn = "actor_id";
        public const string ActorFirstNameColumn = "first_name";
        public const string ActorLastNameColumn = "last_name";
        public const string ActorFROM = $" FROM {ActorTableName}";
        public const string ActorORDERBY = $" ORDER BY {ActorLastNameColumn} ASC, {ActorFirstNameColumn} ASC";
        private string _columnName;
        private string _value;
        public ActorMapping(string columnName, string value)
        {
            _columnName = columnName;
            _value = value;
        }
        public static string GetSelectClause()
        {
            return $"SELECT {ActorIdColumn}, {ActorFirstNameColumn}, {ActorLastNameColumn}";
        }
        public static string GetWhereClause(List<ActorMapping> actorMappings)
        {
            string whereClause = " WHERE 1 = 1";
            foreach (ActorMapping actorMapping in actorMappings)
            {
                whereClause += $" AND {actorMapping._columnName} = @{actorMapping._columnName}";
            }
            return whereClause;
        }
        public string[] GetParameter()
        {
            return [$"@{_columnName}", _value];
        }
    }
}

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
        public string ColumnName { get; private set; }
        public string Value { get; private set; }
        public ActorMapping(string columnName, string value)
        {
            ColumnName = columnName;
            Value = value;
        }
        public string[] GetParameter()
        {
            return [$"@{ColumnName}", Value];
        }
    }
}

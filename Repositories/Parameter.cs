using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Repositories
{
    internal class Parameter
    {
        public string TableName { get; private set; }
        public string ColumnName { get; private set; }
        public string ParameterName { get { return $"@{ColumnName}"; } }
        public string Value { get; private set; }
        public Parameter(string tableName, string columnName, string value)
        {
            TableName = tableName;
            ColumnName = columnName;
            Value = value;
        }
    }
}

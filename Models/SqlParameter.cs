using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Models
{
    internal class SqlParameter
    {
        public string ColumnName { get; private set; }
        public string Name { get { return $"@{ColumnName}"; } }
        public string Value { get; private set; }
        public SqlParameter(string columnName, string value)
        {
            ColumnName = columnName;
            Value = value;
        }
    }
}

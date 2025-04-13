using ADOnetSakilaKoppling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IQueryBuilder
    {
        string GetSelectClause();
        string GetFromClause();
        string GetWhereClause(List<Parameter> parameters);
        string GetOrderByClause();
    }
}

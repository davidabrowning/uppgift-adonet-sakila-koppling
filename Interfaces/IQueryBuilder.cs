using ADOnetSakilaKoppling.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IQueryBuilder
    {
        string GetActorQuery(List<Parameter> parameters);
        string GetFilmQuery(List<Parameter> parameters);
        string GetActorFilmQuery(List<Parameter> parameters);
    }
}

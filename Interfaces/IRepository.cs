using ADOnetSakilaKoppling.Models;
using ADOnetSakilaKoppling.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IRepository
    {
        List<Actor> GetSomeActors(List<Parameter> parameters);
        List<Actor> GetAllActors();
        int LongestFilmTitle();
        int LongestActorName();
    }
}

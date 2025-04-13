using ADOnetSakilaKoppling.Database;
using ADOnetSakilaKoppling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IActorFilmRepository
    {
        public List<Actor> LoadActors(List<Parameter> parameters);
        public List<Actor> LoadActors();
        public List<Film> LoadFilms(List<Parameter> parameters);
        public List<Film> LoadFilms();

    }
}

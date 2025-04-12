using ADOnetSakilaKoppling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IRepository
    {
        List<Actor> GetActorsByFirstName(string firstName);
        List<Actor> GetActorsByLastName(string lastName);
        List<Actor> GetActorsByFullName(string firstName, string lastName);
        List<Actor> GetAllActors();
        int LongestFilmTitle();
        int LongestActorName();
    }
}

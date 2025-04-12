using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IActorService
    {
        void PrintFilmographiesByFirstName();
        void PrintFilmographiesByLastName();
        void PrintFilmographiesByFullName();
        void PrintAllActorNames();
    }
}

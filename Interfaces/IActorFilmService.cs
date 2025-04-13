using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Interfaces
{
    internal interface IActorFilmService
    {
        void PrintFilmographiesByFirstName();
        void PrintFilmographiesByLastName();
        void PrintFilmographiesByFullName();
        void PrintAllActorNames();
    }
}

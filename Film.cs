using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal class Film
    {
        public int FilmId { get; }
        public string Title { get; }
        public Film(int filmId, string title)
        {
            FilmId = filmId;
            Title = title;
        }

        public override string? ToString()
        {
            return Title;
        }
    }
}

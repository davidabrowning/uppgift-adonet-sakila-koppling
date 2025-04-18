﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Database
{
    internal static class ActorFilmMapping
    {
        public const string ActorTableName = "actor";
        public const string ActorIdColumn = "actor_id";
        public const string ActorFirstNameColumn = "first_name";
        public const string ActorLastNameColumn = "last_name";

        public const string FilmTableName = "film";
        public const string FilmIdColumn = "film_id";
        public const string FilmTitleColumn = "title";

        public const string ActorFilmTableName = "film_actor";
        public const string ActorFilmActorIdColumn = "actor_id";
        public const string ActorFilmFilmIdColumn = "film_id";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal class Actor
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public List<Film> Films { get; }
        public Actor(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Films = new List<Film>();
        }
        public void Add(Film film)
        {
            Films.Add(film);
        }
    }
}

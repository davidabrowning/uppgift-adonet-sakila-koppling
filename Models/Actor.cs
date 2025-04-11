using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling.Models
{
    internal class Actor
    {
        public int ActorId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public List<Film> Films { get; }
        public Actor(int actorId, string firstName, string lastName)
        {
            ActorId = actorId;
            FirstName = firstName;
            LastName = lastName;
            Films = new List<Film>();
        }
        public void Add(Film film)
        {
            Films.Add(film);
        }
        public override string? ToString()
        {
            return FullName;
        }
    }
}

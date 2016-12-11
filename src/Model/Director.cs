using System;

namespace StarWars.Model
{
    public class Director
    {
        public int DirectorId { get; set; }
        public int Episode    { get; set; }
        public string Name    { get; set; }
        public DateTime Born  { get; set; }
    }
}
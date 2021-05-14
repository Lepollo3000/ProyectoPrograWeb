using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoPrograWeb.Models
{
    public partial class Breed
    {
        public Breed()
        {
            Pets = new HashSet<Pet>();
        }

        public int IdBreed { get; set; }
        public int IdSpecieRace { get; set; }
        public string NameBreed { get; set; }

        public virtual Specie IdSpecieRaceNavigation { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}

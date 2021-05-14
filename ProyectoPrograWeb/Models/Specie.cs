using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoPrograWeb.Models
{
    public partial class Specie
    {
        public Specie()
        {
            Breeds = new HashSet<Breed>();
            Pets = new HashSet<Pet>();
        }

        public int IdSpecie { get; set; }
        public string NameSpecie { get; set; }

        public virtual ICollection<Breed> Breeds { get; set; }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}

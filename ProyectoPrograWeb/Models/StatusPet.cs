using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoPrograWeb.Models
{
    public partial class StatusPet
    {
        public StatusPet()
        {
            Pets = new HashSet<Pet>();
        }

        public int IdStatus { get; set; }
        public string NameStatus { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}

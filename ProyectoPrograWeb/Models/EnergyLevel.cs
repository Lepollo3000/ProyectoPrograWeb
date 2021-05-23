using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoPrograWeb.Models
{
    public partial class EnergyLevel
    {
        public EnergyLevel()
        {
            Pets = new HashSet<Pet>();
        }

        public int LevelId { get; set; }
        public string LevelName { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}

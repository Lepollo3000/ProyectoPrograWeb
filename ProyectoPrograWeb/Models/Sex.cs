using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoPrograWeb.Models
{
    public partial class Sex
    {
        public Sex()
        {
            Pets = new HashSet<Pet>();
        }

        public int IdSex { get; set; }
        public string NameSex { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}

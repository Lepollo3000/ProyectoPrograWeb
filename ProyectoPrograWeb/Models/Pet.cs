using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoPrograWeb.Models
{
    public partial class Pet
    {
        public int IdPet { get; set; }
        public int IdSpeciePet { get; set; }
        public int? IdBreedPet { get; set; }
        public int AgePet { get; set; }
        public bool IsAgeMonth { get; set; }
        public decimal WeightPet { get; set; }
        public int IdSexPet { get; set; }
        public string NamePet { get; set; }
        public string DescriptionPet { get; set; }
        public string PhotoPathPet { get; set; }
        public int IdStatusPet { get; set; }

        public virtual Breed IdBreedPetNavigation { get; set; }
        public virtual Sex IdSexPetNavigation { get; set; }
        public virtual Specie IdSpeciePetNavigation { get; set; }
        public virtual StatusPet IdStatusPetNavigation { get; set; }
    }
}

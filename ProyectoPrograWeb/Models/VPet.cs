using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoPrograWeb.Models
{
    public partial class VPet
    {
        public string NameBreed { get; set; }
        public string NameSex { get; set; }
        public string NameSpecie { get; set; }
        public string NameStatus { get; set; }
        public int IdPet { get; set; }
        public int IdSpeciePet { get; set; }
        public int IdBreedPet { get; set; }
        public int AgePet { get; set; }
        public bool IsAgeMonth { get; set; }
        public decimal WeightPet { get; set; }
        public int IdSexPet { get; set; }
        public string NamePet { get; set; }
        public string DescriptionPet { get; set; }
        public string PhotoPathPet { get; set; }
        public int IdStatusPet { get; set; }
    }
}

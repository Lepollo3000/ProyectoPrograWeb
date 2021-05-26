using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoPrograWeb.Models
{
    public partial class VAdoptionUser
    {
        public int IdAdoptionRequest { get; set; }
        public string CellphoneUser { get; set; }
        public string ReasonAdoption { get; set; }
        public string WhereWhoAdoption { get; set; }
        public string IdUser { get; set; }
        public int IdPet { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int IdSpeciePet { get; set; }
        public int? IdBreedPet { get; set; }
        public int AgePet { get; set; }
        public bool IsAgeMonth { get; set; }
        public decimal WeightPet { get; set; }
        public int IdSexPet { get; set; }
        public string NamePet { get; set; }
        public string PhotoPathPet { get; set; }
        public int IdStatusPet { get; set; }
        public string NameBreed { get; set; }
        public string NameStatus { get; set; }
        public string NameSex { get; set; }
        public string NameSpecie { get; set; }
    }
}

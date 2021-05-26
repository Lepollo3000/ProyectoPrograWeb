using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoPrograWeb.Models
{
    public partial class AdoptionRequest
    {
        public int IdAdoptionRequest { get; set; }
        public string CellphoneUser { get; set; }
        public string ReasonAdoption { get; set; }
        public string WhereWhoAdoption { get; set; }
        public string IdUser { get; set; }
        public int IdPet { get; set; }
        public int? IdStatusPet { get; set; }
    }
}

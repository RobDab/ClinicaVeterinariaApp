using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaVeterinariaApp.Models
{
    public class AnimalsJSON
    {
        public int IDAnimal { get; set; }

        public string RegisterDate { get; set; }

        public string BirthDate { get; set; }

        public string Name { get; set; }

        public int SpecieID { get; set; }

        public string Color { get; set; }

        public bool HasChip { get; set; }

        public string ChipNumber { get; set; }

        public bool HasOwner { get; set; }

        public string OwnerName { get; set; }

        public string OwnerLastname { get; set; }

        public string UrlPhoto { get; set; }
    }
}
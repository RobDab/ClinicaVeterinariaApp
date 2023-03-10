namespace ClinicaVeterinariaApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Animals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animals()
        {
            Exams = new HashSet<Exams>();
        }

        [Key]
        public int IDAnimal { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RegisterDate { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Tipologia")]
        public int SpecieID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Colore")]
        public string Color { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data di nascita")]
        public DateTime BirthDate { get; set; }

        public bool HasChip { get; set; }

        [StringLength(10)]
        [Display(Name = "Codice microchip")]
        public string ChipNumber { get; set; }

        [Display(Name = "Padrone?")]
        public bool HasOwner { get; set; }

        [StringLength(20)]
        public string OwnerName { get; set; }

        [StringLength(20)]
        public string OwnerLastname { get; set; }

        [StringLength(15)]
        public string UrlPhoto { get; set; }

        public virtual Species Species { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Exams> Exams { get; set; }
    }
}

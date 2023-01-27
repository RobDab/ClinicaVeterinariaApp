namespace ClinicaVeterinariaApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Exams
    {
        [Key]
        public int ExamID { get; set; }

        public int IDAnimal { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Esame")]
        public DateTime ExamDate { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Tipologia esame")]
        public string Exam { get; set; }

        [Required]
        [Display(Name = "Note")]
        public string ExamNotes { get; set; }

        public virtual Animals Animals { get; set; }
    }
}

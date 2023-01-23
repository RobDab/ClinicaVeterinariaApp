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
        public DateTime ExamDate { get; set; }

        [Required]
        [StringLength(30)]
        public string Exam { get; set; }

        [Required]
        public string ExamNotes { get; set; }

        public virtual Animals Animals { get; set; }
    }
}

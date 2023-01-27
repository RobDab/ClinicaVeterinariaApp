using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClinicaVeterinariaApp.Models
{
    public class Email
    {
        [Required]
        [Display(Name = "Email")]
        public string EmailSendUser { get; set; }

        [Display(Name = "Oggetto")]
        public string ObjectMessage { get; set; }

        [Required]
        [Display(Name = "Testo email")]
        public string Message { get; set; }

        [Display(Name = "Allegato")]
        public HttpPostedFileBase Attachment { get; set; }
    }
}
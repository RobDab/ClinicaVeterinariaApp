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
        public string EmailSendUser { get; set; }

        public string ObjectMessage { get; set; }

        [Required]
        public string Message { get; set; }

        public HttpPostedFileBase Attachment { get; set; }
    }
}
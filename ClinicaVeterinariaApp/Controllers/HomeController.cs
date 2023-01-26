using ClinicaVeterinariaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace ClinicaVeterinariaApp.Controllers
{
    public class HomeController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchAnimal()
        {
            return View();
        }

        public JsonResult GetAnimal(string strChip)
        {
            try
            {
                Animals animal = db.Animals.Where(a => a.ChipNumber == strChip).FirstOrDefault();
                if (animal != null)
                {
                    Animals animalToReturn = new Animals
                    {
                        Name = animal.Name,
                        Color = animal.Color,
                        HasOwner = animal.HasOwner,
                        UrlPhoto = animal.UrlPhoto,
                        BirthDate = animal.BirthDate,
                        RegisterDate = animal.RegisterDate,

                    };
                    return Json(animalToReturn, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ViewBag.JsonErr = "Animale non trovato!";
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            finally { }
        }

        public ActionResult SearchAnimals()
        {
            ViewBag.SpecieList = db.Species.ToList();
            return View();
        }

        public JsonResult GetAnimals(string ArrIdParam)
        {
            try
            {

                List<Animals> ListAnimal = new List<Animals>();

                foreach (char SpecieID in ArrIdParam)
                {
                    int SpecieIDInt = (int)Char.GetNumericValue(SpecieID);
                    List<Animals> animals = db.Animals.Where(a => a.SpecieID == SpecieIDInt && a.HasOwner == false).ToList();

                    foreach (var animal in animals)
                    {
                        ListAnimal.Add(animal);
                    }
                }

                


                if (ListAnimal.Count() != 0)
                {

                    return Json(ListAnimal, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ViewBag.JsonErr = "Animali non trovati!";
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            finally { }
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Email e)
        {
            MailAddress sender = new MailAddress("lambotester@outlook.it");
            MailAddress recipient = new MailAddress("lambotester@outlook.it");

            MailMessage message = new MailMessage();
            message.Subject = "Email inviata dal sito da: " + e.EmailSendUser;
            message.Body = e.Message;
            message.From = sender;
            message.To.Add(recipient);

            SmtpClient client = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587, //Recommended port is 587
                EnableSsl = true,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                TargetName= "STARTTLS/smtp.office365.com",
                Credentials = new NetworkCredential("lambotester@outlook.it", "Prove12345"),

            };
            //client.Host = "smtp.office365.com";
            //client.Port = 587;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = false;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("lambotester@outlook.it", "Prova12345");

            if(e.Attachment != null){
            
            e.Attachment.SaveAs(Server.MapPath("/Content/" + e.Attachment.FileName));

            string NameFileToSend = Server.MapPath("/Content/" + e.Attachment.FileName);

            message.Attachments.Add(new Attachment(NameFileToSend));

            }
            client.Send(message);

            return View();
        }
    }
}
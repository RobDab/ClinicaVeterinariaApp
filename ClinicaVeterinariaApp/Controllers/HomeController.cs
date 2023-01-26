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
                    
                    AnimalsJSON animalToReturn = new AnimalsJSON()
                    {
                        IDAnimal = animal.IDAnimal,
                        RegisterDate = animal.RegisterDate.ToString("d"),
                        BirthDate = animal.BirthDate.ToString("d"),
                        Name = animal.Name,
                        SpecieID = animal.SpecieID,
                        Color = animal.Color,
                        ChipNumber = animal.ChipNumber,
                        HasOwner = animal.HasOwner,
                        UrlPhoto = animal.UrlPhoto
                    };

                    if (animal.HasOwner)
                    {
                        animalToReturn.OwnerName = animal.OwnerName;
                        animalToReturn.OwnerLastname = animal.OwnerLastname;    
                    }



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

        public JsonResult GetAnimals(int[] ArrIdParam)
        {
            try
            {

                List<AnimalsJSON> ListAnimal = new List<AnimalsJSON>();
                
                if (ArrIdParam.Count() > 0)
                {
                    foreach (int SpecieID in ArrIdParam)
                    {

                        List<Animals> animals = db.Animals.Where(a => a.SpecieID == SpecieID && a.HasOwner == false).ToList();

                        foreach (Animals animal in animals)
                        {
                            AnimalsJSON animalToReturn = new AnimalsJSON()
                            {
                                IDAnimal = animal.IDAnimal,
                                RegisterDate = animal.RegisterDate.ToString("d"),
                                BirthDate = animal.BirthDate.ToString("d"),
                                Name = animal.Name,
                                SpecieID = animal.SpecieID,
                                Color = animal.Color,
                                HasChip = animal.HasChip,
                                HasOwner = animal.HasOwner,
                                UrlPhoto = animal.UrlPhoto
                            };

                            if (animal.HasChip)
                            {
                                animalToReturn.ChipNumber = animal.ChipNumber;
                            }

                            ListAnimal.Add(animalToReturn);
                        }
                    }
                }
                else
                {
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
                


                if (ListAnimal.Count() != 0)
                {

                    return Json(ListAnimal, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
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
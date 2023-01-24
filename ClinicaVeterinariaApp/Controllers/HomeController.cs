using ClinicaVeterinariaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public JsonResult GetAnimal(string strChip) {
            try
            {
                Animals animal = db.Animals.Where(a => a.ChipNumber == strChip).FirstOrDefault();
                if(animal != null) { 
                Animals animalToReturn = new Animals
                {
                    Name = animal.Name,
                    Color = animal.Color,
                    HasChip = animal.HasChip,
                    HasOwner = animal.HasOwner,
                    ChipNumber = animal.ChipNumber,
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
    }
}
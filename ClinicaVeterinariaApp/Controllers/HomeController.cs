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
}
}
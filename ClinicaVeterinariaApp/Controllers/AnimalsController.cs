using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaVeterinariaApp.Models;

namespace ClinicaVeterinariaApp.Controllers
{
    public class AnimalsController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Animals
        public ActionResult Index()
        {
            var animals = db.Animals.Include(a => a.Species);
            return View(animals.ToList());
        }


        // GET: Animals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animals animals = db.Animals.Find(id);
            if (animals == null)
            {
                return HttpNotFound();
            }
            return View(animals);
        }

        // GET: Animals/Create
        public ActionResult Create()
        {
            ViewBag.SpecieID = new SelectList(db.Species, "SpecieID", "Specie");
            return View();
        }

        // POST: Animals/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDAnimal,Name,SpecieID,Color,BirthDate,HasChip,ChipNumber,HasOwner,OwnerName,OwnerLastname,FileFoto")] Animals animals)
        {
            if (ModelState.IsValid)
            {
                animals.RegisterDate = DateTime.Now;
                if (animals.FileFoto != null)
                {
                    string path = Server.MapPath("/Content/FileUpload/" + animals.FileFoto.FileName);
                    animals.FileFoto.SaveAs(path);
                    animals.UrlPhoto = animals.FileFoto.FileName;
                    db.Animals.Add(animals);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Animals.Add(animals);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.SpecieID = new SelectList(db.Species, "SpecieID", "Specie", animals.SpecieID);
            return View(animals);
        }

        // GET: Animals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animals animals = db.Animals.Find(id);
            if (animals == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecieID = new SelectList(db.Species, "SpecieID", "Specie", animals.SpecieID);

            if(animals.UrlPhoto != null)
            {

                TempData["UrlImg"] = animals.UrlPhoto;
            }
            else
            {
                TempData["UrlImg"] = "No foto";
            }
            return View(animals);
        }

        // POST: Animals/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDAnimal,RegisterDate,Name,SpecieID,Color,BirthDate,HasChip,ChipNumber,HasOwner,OwnerName,OwnerLastname,FileFoto")] Animals animals)
        {
            if (ModelState.IsValid)
            {
                if(animals.FileFoto == null)
                {
                    animals.UrlPhoto = TempData["UrlImg"].ToString();
                }
                else
                {
                    string path = Server.MapPath("/Content/FileUpload/" + animals.FileFoto.FileName);
                    animals.FileFoto.SaveAs(path);
                    animals.UrlPhoto = animals.FileFoto.FileName;
                }

                db.Entry(animals).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.SpecieID = new SelectList(db.Species, "SpecieID", "Specie", animals.SpecieID);
            return View(animals);
        }

        // GET: Animals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animals animals = db.Animals.Find(id);
            if (animals == null)
            {
                return HttpNotFound();
            }
            return View(animals);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Animals animals = db.Animals.Find(id);

            List<Exams> exams = db.Exams.Where(e => e.IDAnimal == id).ToList();

            exams.ForEach(e => db.Exams.Remove(e));
            db.Animals.Remove(animals);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

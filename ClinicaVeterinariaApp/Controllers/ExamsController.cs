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
    public class ExamsController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Exams
        public ActionResult Index()
        {
            var exams = db.Exams.Include(e => e.Animals);
            return View(exams.ToList());
        }

        public ActionResult _VisitList(int? id)
        {
            List<Exams> list = db.Exams
                            .Where(e => e.IDAnimal == id)
                            .ToList();
            return PartialView(list);
        }

        // GET: Exams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exams exams = db.Exams.Find(id);
            if (exams == null)
            {
                return HttpNotFound();
            }
            return View(exams);
        }

        // GET: Exams/Create
        public ActionResult Create()
        {
            ViewBag.IDAnimal = new SelectList(db.Animals, "IDAnimal", "Name");
            return View();
        }

        // POST: Exams/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExamID,IDAnimal,ExamDate,Exam,ExamNotes")] Exams exams)
        {
            if (ModelState.IsValid)
            {
                db.Exams.Add(exams);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDAnimal = new SelectList(db.Animals, "IDAnimal", "Name", exams.IDAnimal);
            return View(exams);
        }

        // GET: Exams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exams exams = db.Exams.Find(id);
            if (exams == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDAnimal = new SelectList(db.Animals, "IDAnimal", "Name", exams.IDAnimal);
            return View(exams);
        }

        // POST: Exams/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExamID,IDAnimal,ExamDate,Exam,ExamNotes")] Exams exams)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exams).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDAnimal = new SelectList(db.Animals, "IDAnimal", "Name", exams.IDAnimal);
            return View(exams);
        }

        // GET: Exams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exams exams = db.Exams.Find(id);
            if (exams == null)
            {
                return HttpNotFound();
            }
            return View(exams);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exams exams = db.Exams.Find(id);
            db.Exams.Remove(exams);
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

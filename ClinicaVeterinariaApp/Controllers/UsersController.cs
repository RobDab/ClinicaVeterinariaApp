using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClinicaVeterinariaApp.Models;

namespace ClinicaVeterinariaApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        [AllowAnonymous]
        public ActionResult Login()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Users user)
        {
            int dbCount = db.Users.Where(u => u.Username == user.Username).Count();
            if(dbCount != 0)

            {
                Users dbUser = db.Users.Where(u => u.Username == user.Username).First();
                if (user.Password == dbUser.Password)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, user.RememberMe);
                    return Redirect(FormsAuthentication.DefaultUrl);
                    // BISOGNA CAMBIARE LA DEFAULT URL DELLA LOGIN PER PORTARE ALLA SEZIONE DI AMMINISTRAZIONE
                }
            }
           

            ViewBag.LoginErr = "Username o Password errata. Riprova";

            return View();
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }



        // GET: Users
        
        public ActionResult Index()
        {
            

            return View(db.Users.ToList());
        }

     

        // GET: Users/Create
        public ActionResult SignIn()
        {
            List<SelectListItem> RolesList = new List<SelectListItem>();

            SelectListItem adminRole = new SelectListItem();
            adminRole.Text = "admin";
            adminRole.Value = "admin";
            RolesList.Add(adminRole);

            SelectListItem userRole = new SelectListItem();
            userRole.Text = "user";
            userRole.Value = "user";
            RolesList.Add(userRole);

            ViewBag.RolesList = RolesList;
            TempData["RolesList"] = RolesList;
            return View();
        }

        // POST: Users/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn([Bind(Include = "UserID,Username,Password,Role")] Users user)
        {
            if (ModelState.IsValid)
            {
                int dbCount = db.Users.Where(u => u.Username == user.Username).Count();
                if(dbCount == 0)
                {

                    db.Users.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.UserExists = "Username non disponibile, provane uno nuovo!";
                    ViewBag.RolesList = TempData["RolesList"];

                    return View();
                }

                
                
            }

            
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            
            return View(users);
        }

        // POST: Users/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,Password,RoleID")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }


        

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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

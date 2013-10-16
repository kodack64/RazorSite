using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication3.Models;

namespace MvcApplication3.Controllers
{
    public class GameController : Controller
    {
        private ProgramDbContext db = new ProgramDbContext();

        //
        // GET: /Default1/

        public ActionResult DashBoard()
        {
			ViewBag.ProgramList = db.ProgramModels.ToList();
            return View(db.ProgramModels.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            ProgramModelProfile programmodelprofile = db.ProgramModels.Find(id);
            if (programmodelprofile == null)
            {
                return HttpNotFound();
            }
            return View(programmodelprofile);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProgramModelProfile programmodelprofile)
        {
            if (ModelState.IsValid)
            {
                db.ProgramModels.Add(programmodelprofile);
                db.SaveChanges();
                return RedirectToAction("DashBoard");
            }

            return View(programmodelprofile);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProgramModelProfile programmodelprofile = db.ProgramModels.Find(id);
            if (programmodelprofile == null)
            {
                return HttpNotFound();
            }
            return View(programmodelprofile);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProgramModelProfile programmodelprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(programmodelprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DashBoard");
            }
            return View(programmodelprofile);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProgramModelProfile programmodelprofile = db.ProgramModels.Find(id);
            if (programmodelprofile == null)
            {
                return HttpNotFound();
            }
            return View(programmodelprofile);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProgramModelProfile programmodelprofile = db.ProgramModels.Find(id);
            db.ProgramModels.Remove(programmodelprofile);
            db.SaveChanges();
            return RedirectToAction("DashBoard");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
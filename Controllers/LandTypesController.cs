using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NL_VAS.Entity;
using NL_VAS.Models;
using PagedList;

namespace NL_VAS.Controllers
{
    public class LandTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LandTypes
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<LandType> model = new List<LandType>();
            var landtypelist = db.LandTypes.ToList();
            PagedList<LandType> models = new PagedList<LandType>(landtypelist, page, pageSize);
            return View(models);
        }

        // GET: LandTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandType landType = db.LandTypes.Find(id);
            if (landType == null)
            {
                return HttpNotFound();
            }
            return View(landType);
        }

        // GET: LandTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LandTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name")] LandType landType)
        {
            if (ModelState.IsValid)
            {
                db.LandTypes.Add(landType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(landType);
        }

        // GET: LandTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandType landType = db.LandTypes.Find(id);
            if (landType == null)
            {
                return HttpNotFound();
            }
            return View(landType);
        }

        // POST: LandTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name")] LandType landType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(landType);
        }

        // GET: LandTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandType landType = db.LandTypes.Find(id);
            if (landType == null)
            {
                return HttpNotFound();
            }
            return View(landType);
        }

        // POST: LandTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LandType landType = db.LandTypes.Find(id);
            db.LandTypes.Remove(landType);
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

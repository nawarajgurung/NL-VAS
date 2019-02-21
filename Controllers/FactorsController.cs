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
using AutoMapper;
using NL_VAS.ViewModel;

namespace NL_VAS.Controllers
{
    public class FactorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Factors
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<Factor> model = new List<Factor>();
            var factorylist = db.Factors.ToList();
            PagedList<Factor> models = new PagedList<Factor>(factorylist, page, pageSize);
            return View(models);
        }

        // GET: Factors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factor factor = db.Factors.Find(id);
            if (factor == null)
            {
                return HttpNotFound();
            }
            return View(factor);
        }

        // GET: Factors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Factors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name")] Factor factor)
        {
            if (ModelState.IsValid)
            {
                db.Factors.Add(factor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(factor);
        }

        // GET: Factors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factor factor = db.Factors.Find(id);
            if (factor == null)
            {
                return HttpNotFound();
            }
            return View(factor);
        }

        // POST: Factors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name")] Factor factor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(factor);
        }

        // GET: Factors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factor factor = db.Factors.Find(id);
            if (factor == null)
            {
                return HttpNotFound();
            }
            return View(factor);
        }

        // POST: Factors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factor factor = db.Factors.Find(id);
            db.Factors.Remove(factor);
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

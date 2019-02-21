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
    public class FactorValuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FactorValues
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<FactorValue> model = new List<FactorValue>();
            var factorValues = db.FactorValues.Include(f => f.Factor).ToList();
            PagedList<FactorValue> models = new PagedList<FactorValue>(factorValues, page, pageSize);
            return View(models);
            
        }

        // GET: FactorValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactorValue factorValue = db.FactorValues.Find(id);
            if (factorValue == null)
            {
                return HttpNotFound();
            }
            return View(factorValue);
        }

        // GET: FactorValues/Create
        public ActionResult Create()
        {
            ViewBag.FactorId = new SelectList(db.Factors, "id", "Name");
            return View();
        }

        // POST: FactorValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,FactorId,Value")] FactorValue factorValue)
        {
            if (ModelState.IsValid)
            {
                db.FactorValues.Add(factorValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FactorId = new SelectList(db.Factors, "id", "Name", factorValue.FactorId);
            return View(factorValue);
        }

        // GET: FactorValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactorValue factorValue = db.FactorValues.Find(id);
            if (factorValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactorId = new SelectList(db.Factors, "id", "Name", factorValue.FactorId);
            return View(factorValue);
        }

        // POST: FactorValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,FactorId,Value")] FactorValue factorValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factorValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FactorId = new SelectList(db.Factors, "id", "Name", factorValue.FactorId);
            return View(factorValue);
        }

        // GET: FactorValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FactorValue factorValue = db.FactorValues.Find(id);
            if (factorValue == null)
            {
                return HttpNotFound();
            }
            return View(factorValue);
        }

        // POST: FactorValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FactorValue factorValue = db.FactorValues.Find(id);
            db.FactorValues.Remove(factorValue);
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

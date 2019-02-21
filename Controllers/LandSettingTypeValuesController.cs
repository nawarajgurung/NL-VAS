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
    [Authorize]
    public class LandSettingTypeValuesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LandSettingTypeValues
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<LandSettingTypeValue> model = new List<LandSettingTypeValue>();
            var landSettingTypeValues = db.LandSettingTypeValues.Include(l => l.LandSettingType).ToList();
            PagedList<LandSettingTypeValue> models = new PagedList<LandSettingTypeValue>(landSettingTypeValues, page, pageSize);
            return View(models);
        }

        // GET: LandSettingTypeValues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandSettingTypeValue landSettingTypeValue = db.LandSettingTypeValues.Find(id);
            if (landSettingTypeValue == null)
            {
                return HttpNotFound();
            }
            return View(landSettingTypeValue);
        }

        // GET: LandSettingTypeValues/Create
        public ActionResult Create()
        {
            ViewBag.LandSettingTypeId = new SelectList(db.LandSettingTypes, "Id", "Name");
            return View();
        }

        // POST: LandSettingTypeValues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Value,LandSettingTypeId")] LandSettingTypeValue landSettingTypeValue)
        {
            if (ModelState.IsValid)
            {
                db.LandSettingTypeValues.Add(landSettingTypeValue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LandSettingTypeId = new SelectList(db.LandSettingTypes, "Id", "Name", landSettingTypeValue.LandSettingTypeId);
            return View(landSettingTypeValue);
        }

        // GET: LandSettingTypeValues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandSettingTypeValue landSettingTypeValue = db.LandSettingTypeValues.Find(id);
            if (landSettingTypeValue == null)
            {
                return HttpNotFound();
            }
            ViewBag.LandSettingTypeId = new SelectList(db.LandSettingTypes, "Id", "Name", landSettingTypeValue.LandSettingTypeId);
            return View(landSettingTypeValue);
        }

        // POST: LandSettingTypeValues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Value,LandSettingTypeId")] LandSettingTypeValue landSettingTypeValue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landSettingTypeValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LandSettingTypeId = new SelectList(db.LandSettingTypes, "Id", "Name", landSettingTypeValue.LandSettingTypeId);
            return View(landSettingTypeValue);
        }

        // GET: LandSettingTypeValues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandSettingTypeValue landSettingTypeValue = db.LandSettingTypeValues.Find(id);
            if (landSettingTypeValue == null)
            {
                return HttpNotFound();
            }
            return View(landSettingTypeValue);
        }

        // POST: LandSettingTypeValues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LandSettingTypeValue landSettingTypeValue = db.LandSettingTypeValues.Find(id);
            db.LandSettingTypeValues.Remove(landSettingTypeValue);
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

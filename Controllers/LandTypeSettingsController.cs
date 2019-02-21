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
    public class LandTypeSettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LandTypeSettings
        public ActionResult Index(int page = 1, int pageSize = 10)
        {

            List<LandTypeSetting> model = new List<LandTypeSetting>();
            var landTypeSettings = db.LandTypeSettings.Include(l => l.LandSetting).Include(l => l.LandType).ToList();
            PagedList<LandTypeSetting> models = new PagedList<LandTypeSetting>(landTypeSettings, page, pageSize);
            return View(models);
            
        }

        // GET: LandTypeSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandTypeSetting landTypeSetting = db.LandTypeSettings.Find(id);
            if (landTypeSetting == null)
            {
                return HttpNotFound();
            }
            return View(landTypeSetting);
        }

        // GET: LandTypeSettings/Create
        public ActionResult Create()
        {
            ViewBag.LandsettingId = new SelectList(db.LandSetting, "Id", "Name");
            ViewBag.LandTypeId = new SelectList(db.LandTypes, "id", "Name");
            return View();
        }

        // POST: LandTypeSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,LandsettingId,LandTypeId,value")] LandTypeSetting landTypeSetting)
        {
            if (ModelState.IsValid)
            {
                db.LandTypeSettings.Add(landTypeSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LandsettingId = new SelectList(db.LandSetting, "Id", "Name", landTypeSetting.LandsettingId);
            ViewBag.LandTypeId = new SelectList(db.LandTypes, "id", "Name", landTypeSetting.LandTypeId);
            return View(landTypeSetting);
        }

        // GET: LandTypeSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandTypeSetting landTypeSetting = db.LandTypeSettings.Find(id);
            if (landTypeSetting == null)
            {
                return HttpNotFound();
            }
            ViewBag.LandsettingId = new SelectList(db.LandSetting, "Id", "Name", landTypeSetting.LandsettingId);
            ViewBag.LandTypeId = new SelectList(db.LandTypes, "id", "Name", landTypeSetting.LandTypeId);
            return View(landTypeSetting);
        }

        // POST: LandTypeSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,LandsettingId,LandTypeId,value")] LandTypeSetting landTypeSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landTypeSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LandsettingId = new SelectList(db.LandSetting, "Id", "Name", landTypeSetting.LandsettingId);
            ViewBag.LandTypeId = new SelectList(db.LandTypes, "id", "Name", landTypeSetting.LandTypeId);
            return View(landTypeSetting);
        }

        // GET: LandTypeSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandTypeSetting landTypeSetting = db.LandTypeSettings.Find(id);
            if (landTypeSetting == null)
            {
                return HttpNotFound();
            }
            return View(landTypeSetting);
        }

        // POST: LandTypeSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LandTypeSetting landTypeSetting = db.LandTypeSettings.Find(id);
            db.LandTypeSettings.Remove(landTypeSetting);
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

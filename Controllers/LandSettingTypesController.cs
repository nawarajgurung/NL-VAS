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
    public class LandSettingTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LandSettingTypes
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<LandSettingType> model = new List<LandSettingType>();
            var landsettingtypelist = db.LandSettingTypes.Include(l => l.LandSetting).ToList();
            PagedList<LandSettingType> models = new PagedList<LandSettingType>(landsettingtypelist, page, pageSize);
            return View(models);

           
        }

        // GET: LandSettingTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandSettingType landSettingType = db.LandSettingTypes.Find(id);
            if (landSettingType == null)
            {
                return HttpNotFound();
            }
            return View(landSettingType);
        }

        // GET: LandSettingTypes/Create
        public ActionResult Create()
        {
            ViewBag.LandSettingID = new SelectList(db.LandSetting, "Id", "Name");
            return View();
        }

        // POST: LandSettingTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Status,LandSettingID")] LandSettingType landSettingType)
        {
            if (ModelState.IsValid)
            {
                db.LandSettingTypes.Add(landSettingType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LandSettingID = new SelectList(db.LandSetting, "Id", "Name", landSettingType.LandSettingID);
            return View(landSettingType);
        }

        // GET: LandSettingTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandSettingType landSettingType = db.LandSettingTypes.Find(id);
            if (landSettingType == null)
            {
                return HttpNotFound();
            }
            ViewBag.LandSettingID = new SelectList(db.LandSetting, "Id", "Name", landSettingType.LandSettingID);
            return View(landSettingType);
        }

        // POST: LandSettingTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Status,LandSettingID")] LandSettingType landSettingType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landSettingType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LandSettingID = new SelectList(db.LandSetting, "Id", "Name", landSettingType.LandSettingID);
            return View(landSettingType);
        }

        // GET: LandSettingTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandSettingType landSettingType = db.LandSettingTypes.Find(id);
            if (landSettingType == null)
            {
                return HttpNotFound();
            }
            return View(landSettingType);
        }

        // POST: LandSettingTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LandSettingType landSettingType = db.LandSettingTypes.Find(id);
            db.LandSettingTypes.Remove(landSettingType);
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

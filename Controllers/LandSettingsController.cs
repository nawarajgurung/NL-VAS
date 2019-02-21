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
    public class LandSettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LandSettings
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<LandSetting> model = new List<LandSetting>();
            var landsettinglist = db.LandSetting.ToList();
            PagedList<LandSetting> models = new PagedList<LandSetting>(landsettinglist, page, pageSize);
            return View(models);
        }

        // GET: LandSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandSetting landSetting = db.LandSetting.Find(id);
            if (landSetting == null)
            {
                return HttpNotFound();
            }
            return View(landSetting);
        }

        // GET: LandSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LandSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] LandSetting landSetting)
        {
            if (ModelState.IsValid)
            {
                db.LandSetting.Add(landSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(landSetting);
        }

        // GET: LandSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandSetting landSetting = db.LandSetting.Find(id);
            if (landSetting == null)
            {
                return HttpNotFound();
            }
            return View(landSetting);
        }

        // POST: LandSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] LandSetting landSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(landSetting);
        }

        // GET: LandSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandSetting landSetting = db.LandSetting.Find(id);
            if (landSetting == null)
            {
                return HttpNotFound();
            }
            return View(landSetting);
        }

        // POST: LandSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LandSetting landSetting = db.LandSetting.Find(id);
            db.LandSetting.Remove(landSetting);
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

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
using NL_VAS.ViewModel;
using PagedList;

namespace NL_VAS.Controllers
{
    public class ConsiderationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult CadestralMap()
        {
            return View();
        }
        // GET: Considerations
        public ActionResult Index(int page = 1, int pageSize = 10)
        {

            List<Consideration> model = new List<Consideration>();
            var factorylist = db.Considerations.Include(c => c.FactorValue).Include(c => c.GeneralInfoParcel).ToList();
            PagedList<Consideration> models = new PagedList<Consideration>(factorylist, page, pageSize);
            return View(models);
        }

        // GET: Considerations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consideration consideration = db.Considerations.Find(id);
            if (consideration == null)
            {
                return HttpNotFound();
            }
            return View(consideration);
        }

        // GET: Considerations/Create
        public ActionResult Create()
        {
            ViewBag.factor = db.Factors.ToList();
            ViewBag.GeneralParcelId = new SelectList(db.GeneralInfoParcels, "Id", "ParcelNo");
            return View();
        }

        // POST: Considerations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConsiderationViewModel consideration)
        {

            var conlist = new List<Consideration>();
            if (ModelState.IsValid)
            {
                foreach(var condata in consideration.considerationList)
                {
                    var con = new Consideration();
                    con.GeneralParcelId = consideration.GeneralParcelId;
                    con.FactorValueId = condata.FactorValueId;
                    var convalue = condata.Value;
                    if (convalue != null)
                    {
                        con.Value = convalue.Value;

                    }
                    else
                    {
                        con.Value = 0;
                    }
                    con.Status = condata.Status;
                    conlist.Add(con);
                }
                db.Considerations.AddRange(conlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.FactorValueId = new SelectList(db.FactorValues, "id", "Name", consideration.FactorValueId);
            //ViewBag.GeneralParcelId = new SelectList(db.GeneralInfoParcels, "Id", "ParcelNo", consideration.GeneralParcelId);
            return View(consideration);
        }

        // GET: Considerations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consideration consideration = db.Considerations.Find(id);
            if (consideration == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactorValueId = new SelectList(db.FactorValues, "id", "Name", consideration.FactorValueId);
            ViewBag.GeneralParcelId = new SelectList(db.GeneralInfoParcels, "Id", "ParcelNo", consideration.GeneralParcelId);
            return View(consideration);
        }

        // POST: Considerations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GeneralParcelId,FactorValueId,Status,Value")] Consideration consideration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consideration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FactorValueId = new SelectList(db.FactorValues, "id", "Name", consideration.FactorValueId);
            ViewBag.GeneralParcelId = new SelectList(db.GeneralInfoParcels, "Id", "ParcelNo", consideration.GeneralParcelId);
            return View(consideration);
        }

        // GET: Considerations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consideration consideration = db.Considerations.Find(id);
            if (consideration == null)
            {
                return HttpNotFound();
            }
            return View(consideration);
        }

        // POST: Considerations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consideration consideration = db.Considerations.Find(id);
            db.Considerations.Remove(consideration);
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

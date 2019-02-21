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
    public class LandUsesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LandUses
        public ActionResult Index(int page=1, int pageSize=10)
        {
            var landuselist = new List<LandUsePViewModel>();
            decimal total=0;
            var landUse = db.LandUse.Include(l => l.GeneralInfoParcel).Include(l => l.LandType).ToList();
            foreach(var land in landUse)
            {
                var landvm = new LandUsePViewModel();
                landvm.id = land.id;
                landvm.ParcelNo = land.GeneralInfoParcel.ParcelNo;
                landvm.LandType = land.LandType.Name;
                landvm.Landuse = new List<LandUseViewModel>();
                var landusevalue = LandUse(land.GeneralInfoParcelId, land.LandTypeId);
                foreach(var lands in landusevalue)
                {
                    var landdetail = new LandUseViewModel();
                    landdetail.LandSetting = lands.LandSetting;
                    landdetail.Value = lands.Value;
                    total = total + lands.Value;
                    landvm.Landuse.Add(landdetail);
                }
                landvm.Total = total;
                landuselist.Add(landvm);
                total = 0;
            }
            PagedList<LandUsePViewModel> models = new PagedList<LandUsePViewModel>(landuselist, page, pageSize);
            return View(models);
        }

        // GET: LandUses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandUse landUse = db.LandUse.Find(id);
            if (landUse == null)
            {
                return HttpNotFound();
            }
            return View(landUse);
        }

        // GET: LandUses/Create
        public ActionResult Create()
        {
            ViewBag.GeneralInfoParcelId = new SelectList(db.GeneralInfoParcels, "Id", "ParcelNo");
            ViewBag.LandTypeId = new SelectList(db.LandTypes, "id", "Name");
            ViewBag.LandSetting = db.LandSetting.ToList();
            return View();
        }

        // POST: LandUses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,GeneralInfoParcelId,LandTypeId")] LandUse landUse)
        {
            if (ModelState.IsValid)
            {
                db.LandUse.Add(landUse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GeneralInfoParcelId = new SelectList(db.GeneralInfoParcels, "Id", "ParcelNo", landUse.GeneralInfoParcelId);
            ViewBag.LandTypeId = new SelectList(db.LandTypes, "id", "Name", landUse.LandTypeId);
            return View(landUse);
        }

        // GET: LandUses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandUse landUse = db.LandUse.Find(id);
            if (landUse == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneralInfoParcelId = new SelectList(db.GeneralInfoParcels, "Id", "ParcelNo", landUse.GeneralInfoParcelId);
            ViewBag.LandTypeId = new SelectList(db.LandTypes, "id", "Name", landUse.LandTypeId);
            return View(landUse);
        }

        // POST: LandUses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,GeneralInfoParcelId,LandTypeId")] LandUse landUse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(landUse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GeneralInfoParcelId = new SelectList(db.GeneralInfoParcels, "Id", "ParcelNo", landUse.GeneralInfoParcelId);
            ViewBag.LandTypeId = new SelectList(db.LandTypes, "id", "Name", landUse.LandTypeId);
            return View(landUse);
        }

        // GET: LandUses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LandUse landUse = db.LandUse.Find(id);
            if (landUse == null)
            {
                return HttpNotFound();
            }
            return View(landUse);
        }

        // POST: LandUses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LandUse landUse = db.LandUse.Find(id);
            db.LandUse.Remove(landUse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult results(int generalid,int landtype)
        {
            var landuseviewmodel = new List<LandUseViewModel>();
            var calculatedvalue = new List<LandUseViewModel>();
            var LandValueScore = db.LandValuationScore.Where(x => x.GeneralinfoparcelID == generalid).ToList();            
            foreach(var landvalue in LandValueScore)
            {
                var landsettingtype = db.LandSettingTypeValues.Where(x => x.Id == landvalue.landsettingvalueID).FirstOrDefault();
                    var landusevalue = new LandUseViewModel();
                   var LandSetting= landsettingtype.LandSettingType.LandSetting;                   
                if (landuseviewmodel.Exists(m => m.LandSettingId == LandSetting.Id))
                {
                    var data = landuseviewmodel.Find(m => m.LandSettingId == LandSetting.Id);
                    landusevalue.LandSettingId = LandSetting.Id;
                    landusevalue.LandSetting = LandSetting.Name;
                    landusevalue.Value = landsettingtype.Value+data.Value;
                    landuseviewmodel.Remove(data);
                }
                else
                {
                    landusevalue.LandSettingId = LandSetting.Id;
                    landusevalue.LandSetting = LandSetting.Name;
                    landusevalue.Value = landsettingtype.Value;
                }
                landuseviewmodel.Add(landusevalue);                  

            }
            var Landtype = db.LandTypes.Where(x => x.id == landtype).FirstOrDefault();
            foreach(var landtypevalue in Landtype.Landtypesetting)
            {
                var landtypedata = new LandUseViewModel();
                var data = landuseviewmodel.Find(m => m.LandSettingId == landtypevalue.LandsettingId);
                if (data != null)
                {
                    landtypedata.LandSettingId = data.LandSettingId;
                    landtypedata.LandSetting = data.LandSetting;
                    landtypedata.Value = data.Value * (landtypevalue.value / 100);
                    calculatedvalue.Add(landtypedata);
                }
                
            }
            return Json(calculatedvalue, JsonRequestBehavior.AllowGet);
        }
        public List<LandUseViewModel> LandUse(int generalid, int landtype)
        {
            var landuseviewmodel = new List<LandUseViewModel>();
            var calculatedvalue = new List<LandUseViewModel>();
            var LandValueScore = db.LandValuationScore.Where(x => x.GeneralinfoparcelID == generalid).ToList();
            foreach (var landvalue in LandValueScore)
            {
                var landsettingtype = db.LandSettingTypeValues.Where(x => x.Id == landvalue.landsettingvalueID).FirstOrDefault();
                var landusevalue = new LandUseViewModel();
                var LandSetting = landsettingtype.LandSettingType.LandSetting;
                if (landuseviewmodel.Exists(m => m.LandSettingId == LandSetting.Id))
                {
                    var data = landuseviewmodel.Find(m => m.LandSettingId == LandSetting.Id);
                    landusevalue.LandSettingId = LandSetting.Id;
                    landusevalue.LandSetting = LandSetting.Name;
                    landusevalue.Value = landsettingtype.Value + data.Value;
                    landuseviewmodel.Remove(data);
                }
                else
                {
                    landusevalue.LandSettingId = LandSetting.Id;
                    landusevalue.LandSetting = LandSetting.Name;
                    landusevalue.Value = landsettingtype.Value;
                }
                landuseviewmodel.Add(landusevalue);

            }
            var Landtype = db.LandTypes.Where(x => x.id == landtype).FirstOrDefault();
            foreach (var landtypevalue in Landtype.Landtypesetting)
            {
                var landtypedata = new LandUseViewModel();
                var data = landuseviewmodel.Find(m => m.LandSettingId == landtypevalue.LandsettingId);
                if (data != null)
                {
                    landtypedata.LandSettingId = data.LandSettingId;
                    landtypedata.LandSetting = data.LandSetting;
                    landtypedata.Value = data.Value * (landtypevalue.value / 100);
                    calculatedvalue.Add(landtypedata);
                }

            }
            return calculatedvalue;
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

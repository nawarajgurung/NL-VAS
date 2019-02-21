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
    [Authorize]
    public class GeneralInfoParcelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GeneralInfoParcels
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<GeneralInfoParcel> model = new List<GeneralInfoParcel>();
            var generalinforparcellist = db.GeneralInfoParcels.ToList();
            PagedList<GeneralInfoParcel> models = new PagedList<GeneralInfoParcel>(generalinforparcellist, page, pageSize);
            return View(models);            
        }

        // GET: GeneralInfoParcels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralInfoParcel generalInfoParcel = db.GeneralInfoParcels.Find(id);
            if (generalInfoParcel == null)
            {
                return HttpNotFound();
            }
            
            return View(generalInfoParcel);
        }

        // GET: GeneralInfoParcels/Create
        public ActionResult Create()
        {
            ViewBag.LandSettingID = db.LandSetting.ToList();
            return View();
        }

        // POST: GeneralInfoParcels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(GeneralInfoParcelViewModel generalInfoParcel)
        {
         
            if (ModelState.IsValid)
            {
                var generalIfo = new GeneralInfoParcel()
                {
                    MapsheetNo = generalInfoParcel.MapsheetNo,
                    OwnerName = generalInfoParcel.OwnerName,
                    Aana = generalInfoParcel.Aana,
                    Daam = generalInfoParcel.Daam,
                    District = generalInfoParcel.District,
                    Paisa = generalInfoParcel.Paisa,
                    ParcelNo = generalInfoParcel.ParcelNo,
                    Ropani = generalInfoParcel.Ropani,
                    VDCMunicipality = generalInfoParcel.VDCMunicipality,
                    Ward = generalInfoParcel.Ward,
                    sqmeter=generalInfoParcel.sqmeter,
                    TotalArea=generalInfoParcel.TotalArea
                };
               var generaldatabase=db.GeneralInfoParcels.Add(generalIfo);
                db.SaveChanges();

                foreach(var type in generalInfoParcel.Types)
                {
                    string[] typedata= type.Split(',');
                    var landscore = new LandValuationScore()
                    {
                        GeneralinfoparcelID = generaldatabase.Id,                       
                        landsettingvalueID = Convert.ToInt32(typedata[0])
                    };
                    db.LandValuationScore.Add(landscore);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(generalInfoParcel);
        }

        // GET: GeneralInfoParcels/Edit/5
        public ActionResult Edit(int? id, int? valid)
        {
            ViewBag.LandSettingID = db.LandSetting.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralInfoParcel generalInfoParcel = db.GeneralInfoParcels.Find(id);
            LandValuationScore landValuationScore = db.LandValuationScore.Where(x=>x.landsettingvalueID==generalInfoParcel.Id).FirstOrDefault();
            if (generalInfoParcel == null)
            {
                return HttpNotFound();
            }
            return View(generalInfoParcel);
        }

        // POST: GeneralInfoParcels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParcelNo,OwnerName,Ropani,Aana,Daam,Paisa,MapsheetNo,District,VDCMunicipality,Ward,LandSettingTypeValue,LandSettingTypeValue,sqmeter,TotalArea")] GeneralInfoParcel generalInfoParcel, [Bind(Include = "Id,LandSettingTypeId")] LandValuationScore landValuationScore)
        {
            if (ModelState.IsValid)
            {
                db.Entry(generalInfoParcel).State = EntityState.Modified;     
                db.Entry(landValuationScore).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            return View(generalInfoParcel);
        }

        // GET: GeneralInfoParcels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralInfoParcel generalInfoParcel = db.GeneralInfoParcels.Find(id);
            if (generalInfoParcel == null)
            {
                return HttpNotFound();
            }
            return View(generalInfoParcel);
        }

        // POST: GeneralInfoParcels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GeneralInfoParcel generalInfoParcel = db.GeneralInfoParcels.Find(id);
            db.GeneralInfoParcels.Remove(generalInfoParcel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetLandSetting(int Id)
        {
            var landsetting = db.LandSettingTypeValues.Where(m => m.Id == Id).ToList();
            return Json(landsetting, JsonRequestBehavior.AllowGet);
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

using NL_VAS.Entity;
using NL_VAS.Models;
using NL_VAS.ViewModel;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NL_VAS.Controllers
{
    public class PriceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {

            ViewBag.GeneralParcel = new SelectList(db.GeneralInfoParcels, "Id", "ParcelNo");
            ViewBag.PriceSetting =new SelectList(db.PriceSetting.ToList(), "perunitprice", "ParcelNo");
            return View();
        }
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var price = new PriceSetting();
                price.generalparcelID = Convert.ToInt32(Request.Form["GeneralParcel"]);
                price.perunitprice = Convert.ToDecimal(Request.Form["PerUnitPrice"]);
                price.AskedPrice = Convert.ToDecimal(Request.Form["AskedPrice"]);
                price.ParcelNo = db.GeneralInfoParcels.Where(m => m.Id == price.generalparcelID).FirstOrDefault().ParcelNo;
                db.PriceSetting.Add(price);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                TempData["error"] = "Price Setting Already Exits";
                return RedirectToAction(nameof(Index));

            }
        }

        public JsonResult TotalScore(int id)
        {
            var total = GetTotalScore(id);
            return Json(total,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TotalScoreResult(string id, decimal price)
        {
            var generalparcel = db.GeneralInfoParcels.ToList();
            var totalscoreresult = new List<PriceViewModel>();
            foreach (var general in generalparcel)
            {
                var pricedetail = new PriceViewModel();
                var askedPrice = db.PriceSetting.Where(x => x.generalparcelID == general.Id).FirstOrDefault();
                pricedetail.ParacelNo = general.ParcelNo;
                pricedetail.Name = general.OwnerName;
                pricedetail.MapSheetNo = general.MapsheetNo;
                pricedetail.NetScore =Math.Round(GetTotalScore(general.Id),4);
                pricedetail.BaseId = id;
                pricedetail.PerUnitPrice =Math.Round(askedPrice.AskedPrice / pricedetail.NetScore,4);
                pricedetail.PerUnitSquareMeterPrice =Math.Round(price / general.sqmeter,4);
                pricedetail.PerSquaremeterPrice =Math.Round(GetTotalScore(general.Id) * pricedetail.PerUnitSquareMeterPrice,4);
                pricedetail.TotalPrice =Math.Round(general.TotalArea * pricedetail.PerSquaremeterPrice,4);

                totalscoreresult.Add(pricedetail);
            }
            return Json(totalscoreresult,JsonRequestBehavior.AllowGet);
        }


        public decimal GetTotalScore(int id)
        {
            var reportsdata = new ReportsController();
            var reports = new ReportsViewModel();
            reports.DataEntry = reportsdata.DataEntryResult(id);
            reports.LandUse = reportsdata.LandUse(id);
            reports.Factor = reportsdata.FactorResult(id);

            decimal total = reports.LandUse.Sum(m => m.Value) + reports.DataEntry.Sum(m => m.Value);

            foreach (var factor in reports.Factor)
            {
                if (factor.status)
                {
                    total = total + (reports.DataEntry.Sum(m => m.Value) + reports.LandUse.Sum(m => m.Value)) * factor.percentage / 100;
                }
                else
                {
                    total = total - (reports.DataEntry.Sum(m => m.Value) + reports.LandUse.Sum(m => m.Value)) * factor.percentage / 100;

                }
            }
            return total;
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
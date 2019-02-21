using NL_VAS.Models;
using NL_VAS.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NL_VAS.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Rating
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            //ViewBag.GeneralParcelId = new SelectList(db.GeneralInfoParcels, "Id", "ParcelNo");

            List<RatingViewModel> RatingList = new List<RatingViewModel>();
            foreach (var general in db.GeneralInfoParcels.ToList())
            {
                var score = db.LandValuationScore.Where(m => m.GeneralinfoparcelID == general.Id).ToList();
                var total = 0;
                foreach (var scoreTotal in score)
                {
                    total = total + db.LandSettingTypeValues.Where(m => m.Id == scoreTotal.landsettingvalueID).FirstOrDefault().Value;
                }
                var rating = new RatingViewModel()
                {
                    id = general.Id,
                    ParcelNo = general.ParcelNo,
                    OwnerName = general.OwnerName,
                    Total = total
                };
                RatingList.Add(rating);
                total = 0;
            }
            PagedList<RatingViewModel> models = new PagedList<RatingViewModel>(RatingList, page, pageSize);
            return View(models);
        }
        public ActionResult Details(int id)
        {
            var data = db.GeneralInfoParcels.Where(m => m.Id == id).FirstOrDefault();
            var reports = new ReportsViewModel();
            reports.GeneralInfoParcel = data;
            reports.DataEntry = DataEntryResult(id);
            reports.LandUse = LandUse(id);
            reports.Factor = FactorResult(id);
            return View(reports);
        }


        public List<DataentryViewModel> DataEntryResult(int id)
        {
            var Dataentrylist = new List<DataentryViewModel>();
            var data = db.LandValuationScore.Where(m => m.GeneralinfoparcelID == id).ToList();
            foreach (var landvalue in data)
            {
                var landvaluedata = db.LandSettingTypeValues.Where(m => m.Id == landvalue.landsettingvalueID).FirstOrDefault();
                var dataentry = new DataentryViewModel();
                if (Dataentrylist.Exists(m => m.id == landvaluedata.LandSettingType.LandSetting.Id))
                {
                    var datalist = Dataentrylist.Find(m => m.id == landvaluedata.LandSettingType.LandSetting.Id);
                    dataentry.id = landvaluedata.LandSettingType.LandSetting.Id;
                    dataentry.Name = landvaluedata.LandSettingType.LandSetting.Name;
                    dataentry.Value = datalist.Value + landvaluedata.Value;
                    Dataentrylist.Remove(datalist);
                }
                else
                {
                    dataentry.id = landvaluedata.LandSettingType.LandSetting.Id;
                    dataentry.Name = landvaluedata.LandSettingType.LandSetting.Name;
                    dataentry.Value = landvaluedata.Value;
                }
                Dataentrylist.Add(dataentry);
            }
            return Dataentrylist;
        }
         public List<LandUseViewModel> LandUse(int id)
        {
            var landuseviewmodel = new List<LandUseViewModel>();
            var calculatedvalue = new List<LandUseViewModel>();
            var landUse = db.LandUse.Where(m=>m.GeneralInfoParcelId==id).ToList();
            foreach (var land in landUse)
            {
                var LandValueScore = db.LandValuationScore.Where(x => x.GeneralinfoparcelID == land.GeneralInfoParcelId).ToList();
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
                var Landtype = db.LandTypes.Where(x => x.id == land.LandTypeId).FirstOrDefault();
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
            }
            return calculatedvalue;
        }

        public List<ViewModel.FactorViewModel> FactorResult(int id)
        {
            var factorlist = new List<ViewModel.FactorViewModel>();
            var data = db.Considerations.Where(m => m.GeneralParcelId == id).ToList();
            foreach(var factor in data)
            {
                var factordata = new ViewModel.FactorViewModel();
                if (factorlist.Exists(m => m.status == true)&& factor.Status)
                {
                    var datas = factorlist.Find(m => m.status==true);
                    
                        factordata.id = factor.Id;
                        factordata.Name = "Increment";
                        factordata.percentage = datas.percentage + factor.Value;
                        factordata.status = factor.Status;
                        factorlist.Remove(datas);
                    
                }
                else if (factorlist.Exists(m => m.status==false) && factor.Status==false)
                {
                    var datas = factorlist.Find(m => m.status==false);
                    if (!factor.Status)
                    {
                        factordata.id = factor.Id;
                        factordata.Name = "Decrement";
                        factordata.percentage = datas.percentage + factor.Value;
                        factordata.status = factor.Status;
                        factorlist.Remove(datas);

                    }
                }
                else
                {
                    if (factor.Status)
                    {
                        factordata.id = factor.Id;
                        factordata.Name = "Increment";
                        factordata.percentage = factor.Value;
                        factordata.status = factor.Status;
                    }
                    else
                    {
                        factordata.id = factor.Id;
                        factordata.Name = "Decrement";
                        factordata.percentage = factor.Value;
                        factordata.status = factor.Status;
                    }

                }
                
                    factorlist.Add(factordata);
            }
            return factorlist;
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
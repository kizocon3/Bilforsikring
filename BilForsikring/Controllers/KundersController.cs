using BilForsikring.Models.ViewModel.Forsiking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BilForsikring.Controllers
{
    public class KundersController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }  

        public ActionResult Prisberegning()
        {
            var bonus = new SelectList(new[] { "10", "15", "20", "25", "30", "40", "50", "60", "70", "90" });
            ViewBag.Message = "Your application description page.";
            ViewBag.PorcentageBonus = bonus;
            return View();
        }
    
        [HttpPost] 
        public ActionResult AddOrEditAsync(KunderViewModel kunder)
        {
            if (kunder.Id == null)
            {
                using (var client = new HttpClient())
                {
                   var uri = new Uri("http://localhost:50926/api/kunder");
                    var json = new JavaScriptSerializer().Serialize(kunder);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(uri, stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
               
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Guid id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Kunder/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
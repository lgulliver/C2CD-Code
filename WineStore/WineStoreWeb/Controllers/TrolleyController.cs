using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WineStoreWeb.Data;
using WineStoreWeb.Models;

namespace WineStoreWeb.Controllers
{
    public class TrolleyController : Controller
    {
        public IActionResult Index()
        {
            ViewData["TrolleyItems"] = TrolleyProxy.GetInstance().GetCurrentNumberOfItems(HttpContext.Session.Id.ToString());

            var trolleyModel = new TrolleyViewModel();
            var trolleyItems = TrolleyProxy.GetInstance().GetCurrentTrolleyItems(HttpContext.Session.Id);

            Dictionary<string, int> preDict = new Dictionary<string, int>();
            foreach(var item in trolleyItems) {
                if(string.IsNullOrWhiteSpace(item)) {
                    continue;
                }

                if(preDict.ContainsKey(item)) {
                    preDict[item]++;
                } else {
                    preDict.Add(item, 1);
                }
            }

            double total = 0.0;
            foreach(var key in preDict.Keys) {
                var wine = InventoryProxy.GetInstance().GetInventoryItem(key);
                trolleyModel.AddTrolleyItemToDisplay(wine, preDict[key]);
                total = total + (wine.WinePrice * preDict[key]);
            }

            ViewBag["Total"] = total.ToString();

            return View(trolleyModel);
        }
    }
}
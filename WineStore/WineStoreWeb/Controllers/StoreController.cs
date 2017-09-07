using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineStoreWeb.Data;
using WineStoreWeb.Models;

namespace WineStoreWeb.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            ViewData["TrolleyItems"] = TrolleyProxy.GetInstance().GetCurrentNumberOfItems(HttpContext.Session.Id.ToString());
            var inventory = InventoryProxy.GetInstance().GetInventory();

            var storeModel = new StoreViewModel();

            foreach(var key in inventory.Keys) {
                storeModel.AddWineToDisplay(inventory[key]);
            }


            return View(storeModel);
        }
    }
}
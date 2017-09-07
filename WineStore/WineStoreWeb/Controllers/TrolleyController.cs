using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineStoreWeb.Data;
using WineStoreWeb.Models;

namespace WineStoreWeb.Controllers
{
    public class TrolleyController : Controller
    {
        public IActionResult Index()
        {
            ViewData["TrolleyItems"] = TrolleyProxy.GetInstance().GetCurrentNumberOfItems(HttpContext.Session.Id.ToString());

            return View(new TrolleyViewModel());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineStoreWeb.Models;
using WineStoreWeb.Data;
using Microsoft.Extensions.Options;

namespace WineStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            ViewData["TrolleyItems"] = TrolleyProxy.GetInstance().GetCurrentNumberOfItems(HttpContext.Session.Id.ToString());
            
            return View();
        }

        public IActionResult About()
        {
            ViewData["TrolleyItems"] = TrolleyProxy.GetInstance().GetCurrentNumberOfItems(HttpContext.Session.Id.ToString());
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["TrolleyItems"] = TrolleyProxy.GetInstance().GetCurrentNumberOfItems(HttpContext.Session.Id.ToString());
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

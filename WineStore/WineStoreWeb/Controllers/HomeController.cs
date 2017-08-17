using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineStoreWeb.Models;

namespace WineStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["CartItems"] = "0";

            return View();
        }

        public IActionResult About()
        {
            ViewData["CartItems"] = "0";
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["CartItems"] = "0";
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

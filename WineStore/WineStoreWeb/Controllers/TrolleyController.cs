﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WineStoreWeb.Models;

namespace WineStoreWeb.Controllers
{
    public class TrolleyController : Controller
    {
        public IActionResult Index()
        {
            return View(new TrolleyViewModel());
        }
    }
}
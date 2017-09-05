using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WinStoreInventory.Controllers
{
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        string[] wines = { "Red1", "Red2", "Red3", "White1", "White2", "White3", "Rose1", "Rose2" };

        // GET api/inventory
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return wines;
        }

        // GET api/inventory/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return wines[id];
        }
    }
}

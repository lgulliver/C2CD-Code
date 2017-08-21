using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WineStoreApi.Controllers
{
    [Route("api/[controller]")]
    public class PurchaseController : Controller
    {

        // GET api/purchase/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/purchase
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // DELETE api/purchase/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

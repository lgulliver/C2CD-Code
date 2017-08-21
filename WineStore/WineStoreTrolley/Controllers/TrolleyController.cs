using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WineStoreTrolley.Controllers
{
    [Route("api/[controller]")]
    public class TrolleyController : Controller
    {
        // GET api/trolley
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[];
        }

        // GET api/trolley/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "empty";
        }

        // POST api/trolley
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/trolley/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/trolley/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

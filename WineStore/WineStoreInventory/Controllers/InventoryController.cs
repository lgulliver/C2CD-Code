using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WineStoreShared;
using WineStoreInventory.Data;

namespace WineStoreInventory.Controllers
{
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly APIOptions _options;
        private InventoryBroker _broker;

        public InventoryController(IOptions<APIOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
            _broker = new InventoryBroker();
            _broker.PullAsync(_options.StorageConnectionString).Wait();
        }

        // POST api/inventory
        [HttpPost]
        public Dictionary<string, WineItem> Post([FromBody]APIPackage package)
        {
            var sessionId = package.sessionIdentifier;
            var apiKey = package.apiKey;

            if (apiKey == null)
            {
                return new Dictionary<string, WineItem>();
            }

            if (!apiKey.Equals(_options.MyAPIKey))
            {
                return new Dictionary<string, WineItem>();
            }

            return _broker.GetCurrentInventory();
        }

        // GET api/inventory/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return null;
        }
    }
}

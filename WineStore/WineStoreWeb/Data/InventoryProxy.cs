using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WineStoreWeb.Data
{
    public class InventoryProxy : Proxy
    {
        private static Proxy _instance;

        public InventoryProxy(string endpoint, string password): base(endpoint, password)
        {
            _instance = this;
        }

        public static InventoryProxy GetInstance()
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("Singelton not instantiated at app startup.");
            }

            return (InventoryProxy) _instance;
        }

        public int GetCurrentNumberOfItems()
        {
            HttpClient client = new HttpClient();
            return 0;
        }
    }
}

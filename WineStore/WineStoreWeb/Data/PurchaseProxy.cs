using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WineStoreWeb.Data
{
    public class PurchaseProxy : Proxy
    {
        private static Proxy _instance;

        public PurchaseProxy(string endpoint, string password) : base(endpoint, password)
        {
            _instance = this;
        }

        public static PurchaseProxy GetInstance()
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("Singelton not instantiated at app startup.");
            }

            return (PurchaseProxy) _instance;
        }

        public int GetCurrentNumberOfItems()
        {
            HttpClient client = new HttpClient();
            return 0;
        }
    }
}

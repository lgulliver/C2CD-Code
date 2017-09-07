using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using WineStoreShared;

namespace WineStoreInventory.Data
{
    public class InventoryBroker
    {
        private Dictionary<string, WineItem> winesInStock;
        private bool hasInitialised = false;

        public InventoryBroker()
        {

        }

        public async Task PullAsync(string storageConnectionString)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(storageConnectionString);
            CloudTableClient tableClient = storage.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("inventory");
            await table.CreateIfNotExistsAsync();

            TableQuery<WineEntity> query = new TableQuery<WineEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.NotEqual, null));
            var queryResult = await table.ExecuteQuerySegmentedAsync(query, null);

            Dictionary<string, WineItem> winesInStorageAccount = new Dictionary<string, WineItem>();
            foreach (var result in queryResult)
            {
                winesInStorageAccount.Add(result.PartitionKey + ":" + result.RowKey, new WineItem(result.PartitionKey, result.RowKey, result.WineName, result.WinePicture, result.WineInfo, result.WinePrice, result.WineInStock));
            }

            winesInStock = winesInStorageAccount;

            hasInitialised = true;
        }

        public async Task UpdateEntityAsync(WineItem changedItem, string storageConnectionString) {
            CloudStorageAccount storage = CloudStorageAccount.Parse(storageConnectionString);
            CloudTableClient tableClient = storage.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("inventory");
            await table.CreateIfNotExistsAsync();

            var operation = TableOperation.Retrieve<WineEntity>(changedItem.WineType, changedItem.WineId);
            var result = await table.ExecuteAsync(operation);
            var retrievedEntity = (WineEntity)result.Result;

            retrievedEntity.WineInfo = changedItem.WineInfo;
            retrievedEntity.WineName = changedItem.WineName;
            retrievedEntity.WinePrice = changedItem.WinePrice;
            retrievedEntity.WineInStock = changedItem.WineInStock;
            retrievedEntity.WinePicture = changedItem.WinePicture;

            operation = TableOperation.Replace(retrievedEntity);
            await table.ExecuteAsync(operation);
        }

        public Dictionary<string, WineItem> GetCurrentInventory() {
            if(hasInitialised) {
                return winesInStock;
            }

            throw new InvalidOperationException();
        }

        private class WineEntity : TableEntity {

            public WineEntity() { }

            public WineEntity(WineItem item) {
                this.PartitionKey = item.WineType;
                this.RowKey = item.WineId;
                this.WineName = item.WineName;
                this.WinePrice = item.WinePrice;
                this.WineInStock = item.WineInStock;
                this.WinePicture = item.WinePicture;
                this.WineInfo = item.WineInfo;
            }

            public string WineName { get; set; }
            public double WinePrice { get; set; }
            public int WineInStock { get; set; }
            public string WinePicture { get; set; }
            public string WineInfo { get; set; }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using WineStoreShared;

namespace WineStoreTrolley.Controllers
{
    [Route("api/[controller]")]
    public class TrolleyController : Controller
    {
        private readonly APIOptions _options;

        public TrolleyController(IOptions<APIOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        // POST api/trolley
        [HttpPost]
        public string Post([FromBody]APIPackage package)
        {
            var sessionId = package.sessionIdentifier;
            var apiKey = package.apiKey;

            if (sessionId == null || apiKey == null)
            {
                return "-1";
            }

            if (!apiKey.Equals(_options.MyAPIKey))
            {
                return "-1";
            }

            CloudStorageAccount storage = CloudStorageAccount.Parse(_options.StorageConnectionString);
            CloudTableClient tableClient = storage.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("trollies");
            table.CreateIfNotExistsAsync().Wait();

            TableQuery<TableEntity> query = new TableQuery<TableEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, sessionId));

            var queryResult = table.ExecuteQuerySegmentedAsync(query, null);
            queryResult.Wait();

            int parsedInt = 0;

            foreach (var entity in queryResult.Result)
            {
                int.TryParse(entity.RowKey, out parsedInt);
            }

            return parsedInt.ToString();
        }

        // PUT api/trolley/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/trolley/5
        [HttpDelete]
        public void Delete([FromBody]string value)
        {
        }
    }

    public class TrolleyEntity : TableEntity
    {
        public TrolleyEntity(string sessionId, string content)
        {
            this.PartitionKey = sessionId;
            this.RowKey = content;
        }

        public TrolleyEntity()
        {

        }

    }


}
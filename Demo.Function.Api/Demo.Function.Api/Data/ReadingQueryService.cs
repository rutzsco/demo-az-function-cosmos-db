using Demo.Function.Api.Model;

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Function.Api.Data
{
    public class ReadingQueryService
    {
        private CosmosClient _cosmosClient;

        public ReadingQueryService(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<IEnumerable<Reading>> GetAll()
        {
            var container = this._cosmosClient.GetContainer("MeasurementDB", "Readings");
            QueryDefinition query = new QueryDefinition("SELECT * FROM C");
            List<Reading> results = new List<Reading>();
            using (FeedIterator<Reading> resultSetIterator = container.GetItemQueryIterator<Reading>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    Microsoft.Azure.Cosmos.FeedResponse<Reading> response = await resultSetIterator.ReadNextAsync();
                    results.AddRange(response);
                }
            }
            return results;
        }
        public async Task<Reading> GetById(string id, string accountId)
        {
            var container = this._cosmosClient.GetContainer("MeasurementDB", "Readings");
            var result = await container.ReadItemAsync<Reading>(id, new PartitionKey(accountId));
            return result;
        }
    }
}

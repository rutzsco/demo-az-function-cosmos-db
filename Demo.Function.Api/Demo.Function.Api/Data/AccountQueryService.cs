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
    public class AccountQueryService
    {
        private CosmosClient _cosmosClient;

        public AccountQueryService(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            var container = this._cosmosClient.GetContainer("MeasurementDB", "Accounts");
            QueryDefinition query = new QueryDefinition("SELECT * FROM C");
            List<Account> results = new List<Account>();
            using (FeedIterator<Account> resultSetIterator = container.GetItemQueryIterator<Account>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    Microsoft.Azure.Cosmos.FeedResponse<Account> response = await resultSetIterator.ReadNextAsync();
                    results.AddRange(response);
                }
            }
            return results;
        }
        public async Task<Account> GetById(string id)
        {
            var container = this._cosmosClient.GetContainer("MeasurementDB", "Accounts");
            var result = await container.ReadItemAsync<Account>(id, new PartitionKey(id));
            return result;
        }
    }
}

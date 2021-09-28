using Demo.Function.Api.Model;
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
        private readonly Uri _serviceEndpoint;
        private readonly string _authKey;

        public AccountQueryService(string connectionString)
        {
            var builder = new DbConnectionStringBuilder() { ConnectionString = connectionString };

            if (builder.TryGetValue("AccountKey", out object key))
            {
                _authKey = key.ToString();
            }

            if (builder.TryGetValue("AccountEndpoint", out object uri))
            {
                _serviceEndpoint = new Uri(uri.ToString());
            }
        }

        public async Task<IEnumerable<Account>> GetListing()
        {
            using (var client = new DocumentClient(_serviceEndpoint, _authKey))
            {
                var collectionLink = UriFactory.CreateDocumentCollectionUri("MeasurementsDB", "Accounts");
                var query = client.CreateDocumentQuery<Account>(collectionLink, new FeedOptions { EnableCrossPartitionQuery = true })
                    .Where(x => x.ItemType == "Account").AsDocumentQuery();

                return await GetAllResultsAsync(query);
            }
        }

  
        private async static Task<T[]> GetAllResultsAsync<T>(IDocumentQuery<T> queryAll)
        {
            var list = new List<T>();
            while (queryAll.HasMoreResults)
            {
                var docs = await queryAll.ExecuteNextAsync<T>();
                foreach (var d in docs)
                {
                    list.Add(d);
                }
            }

            return list.ToArray();
        }
    }
}

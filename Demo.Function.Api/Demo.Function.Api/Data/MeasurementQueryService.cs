﻿using Demo.Function.Api.Model;

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
    public class MeasurementQueryService
    {
        private readonly CosmosClient _cosmosClient;
        private static readonly string DatabaseName = "MeasurementDB";
        private static readonly string ContainerName = "Readings";

        public MeasurementQueryService(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }

        public async Task<IEnumerable<Measurement>> GetAll(string accountId)
        {
            var container = this._cosmosClient.GetContainer(DatabaseName, ContainerName);
            QueryDefinition query = new QueryDefinition("SELECT * FROM Readings r WHERE r.id = @accountId")
               .WithParameter("@accountId", accountId);

            List<Measurement> results = new List<Measurement>();
            using (FeedIterator<Measurement> resultSetIterator = container.GetItemQueryIterator<Measurement>(query))
            {
                while (resultSetIterator.HasMoreResults)
                {
                    Microsoft.Azure.Cosmos.FeedResponse<Measurement> response = await resultSetIterator.ReadNextAsync();
                    results.AddRange(response);
                }
            }
            return results;
        }

        public async Task<Measurement> GetById(string id, string accountId)
        {
            var container = this._cosmosClient.GetContainer(DatabaseName, ContainerName);
            var result = await container.ReadItemAsync<Measurement>(id, new PartitionKey(accountId));
            return result;
        }
    }
}
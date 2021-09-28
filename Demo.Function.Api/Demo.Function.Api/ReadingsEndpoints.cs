using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Demo.Function.Api.Data;
using Microsoft.Azure.Cosmos;
using Demo.Function.Api.Model;

namespace Demo.Function.Api
{
    public class ReadingsEndpoints
    {
        private CosmosClient _cosmosClient;

        public ReadingsEndpoints(CosmosClient cosmosClient)
        {
            this._cosmosClient = cosmosClient;
        }

        [FunctionName("ReadingGetEndpoint")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "account/{accountId}/reading/{id}")] HttpRequest req, string id, string accountId, ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queryService = new ReadingQueryService(_cosmosClient);
            var listing = await queryService.GetById(id, accountId);

            return new OkObjectResult(listing);
        }

        [FunctionName("ReadingCreateEndpoint")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "reading")] HttpRequest req,
            [CosmosDB(databaseName: "MeasurementDB", collectionName: "Readings", ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<Reading> documents,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Convert to request object
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var reading = JsonConvert.DeserializeObject<Reading>(requestBody);

            // Save
            await documents.AddAsync(reading);

            return new OkObjectResult(reading.Id);
        }
    }
}

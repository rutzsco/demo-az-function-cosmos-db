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
    public class MeasurementEndpoints
    {
        private CosmosClient _cosmosClient;

        public MeasurementEndpoints(CosmosClient cosmosClient)
        {
            this._cosmosClient = cosmosClient;
        }

        [FunctionName("MeasurementGetAllEndpoint")]
        public async Task<IActionResult> GetAll([HttpTrigger(AuthorizationLevel.Function, "get", Route = "account/{accountId}/measurement")] HttpRequest req, string accountId, ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queryService = new MeasurementQueryService(_cosmosClient);
            var listing = await queryService.GetAll(accountId);

            return new OkObjectResult(listing);
        }

        [FunctionName("MeasurementGetEndpoint")]
        public async Task<IActionResult> Get([HttpTrigger(AuthorizationLevel.Function, "get", Route = "account/{accountId}/measurement/{id}")] HttpRequest req, string id, string accountId, ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queryService = new MeasurementQueryService(_cosmosClient);
            var listing = await queryService.GetById(id, accountId);

            return new OkObjectResult(listing);
        }

        [FunctionName("MeasurementCreateEndpoint")]
        public static async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Function, "post", Route = "measurement")] HttpRequest req,
            [CosmosDB(databaseName: "MeasurementDB", collectionName: "Measurements", ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<Measurement> documents,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Convert to request object
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var measurement = JsonConvert.DeserializeObject<Measurement>(requestBody);

            // Save
            await documents.AddAsync(measurement);

            return new OkObjectResult(measurement.Id);
        }
    }
}

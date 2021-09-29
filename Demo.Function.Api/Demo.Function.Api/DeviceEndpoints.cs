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
    public class DeviceEndpoints
    {
        private CosmosClient _cosmosClient;

        public DeviceEndpoints(CosmosClient cosmosClient)
        {
            this._cosmosClient = cosmosClient;
        }

        [FunctionName("DeviceGetAllEndpoint")]
        public async Task<IActionResult> GetAll([HttpTrigger(AuthorizationLevel.Function, "get", Route = "account/{accountId}/device")] HttpRequest req, string accountId, ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queryService = new MeasurementQueryService(_cosmosClient);
            var listing = await queryService.GetAll(accountId);

            return new OkObjectResult(listing);
        }

        [FunctionName("DeviceGetEndpoint")]
        public async Task<IActionResult> Get([HttpTrigger(AuthorizationLevel.Function, "get", Route = "account/{accountId}/device/{id}")] HttpRequest req, string id, string accountId, ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queryService = new MeasurementQueryService(_cosmosClient);
            var listing = await queryService.GetById(id, accountId);

            return new OkObjectResult(listing);
        }

        [FunctionName("DeviceCreateEndpoint")]
        public static async Task<IActionResult> Create([HttpTrigger(AuthorizationLevel.Function, "post", Route = "device")] HttpRequest req,
            [CosmosDB(databaseName: "MeasurementDB", collectionName: "Readings", ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<Device> documents,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Convert to request object
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var device = JsonConvert.DeserializeObject<Device>(requestBody);
 
            // Save
            await documents.AddAsync(device);

            return new OkObjectResult(device.Id);
        }
    }
}

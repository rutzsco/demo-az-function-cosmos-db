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

namespace Demo.Function.Api
{
    public class AccountGetAllEndpoint
    {
        private CosmosClient _cosmosClient;

        public AccountGetAllEndpoint(CosmosClient cosmosClient)
        {
            this._cosmosClient = cosmosClient;
        }

        [FunctionName("AccountListEndpoint")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "account")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var queryService = new AccountQueryService(_cosmosClient);
            var listing = await queryService.GetAll();

            return new OkObjectResult(listing);
        }
    }
}

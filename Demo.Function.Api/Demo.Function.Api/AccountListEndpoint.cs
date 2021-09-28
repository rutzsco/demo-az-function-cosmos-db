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

namespace Demo.Function.Api
{
    public static class AccountListEndpoint
    {
        [FunctionName("AccountListEndpoint")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var config = context.BuildConfiguraion();
            var cosmosDBConnection = config["CosmosDBConnection"];


            var queryService = new AccountQueryService(cosmosDBConnection);
            var listing = await queryService.GetListing();

            return new OkObjectResult(listing);
        }
    }
}

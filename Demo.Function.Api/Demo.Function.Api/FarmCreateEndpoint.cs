using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Demo.Function.Api.Model;

namespace Demo.Function.Api
{
    public static class FarmCreateEndpoint
    {
        [FunctionName("FarmCreateEndpoint")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, 
            [CosmosDB(databaseName: "FarmDB", collectionName: "Farms", ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<Farm> documents,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Convert to request object
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var farmDetails = JsonConvert.DeserializeObject<FarmDetails>(requestBody);

            // Map to document object
            if (string.IsNullOrEmpty(farmDetails.FarmName))
                return new BadRequestObjectResult("Farm Name is required.");

            var farmDocument = new Farm(farmDetails);

            // Save
            await documents.AddAsync(farmDocument);

            return new OkObjectResult(farmDocument.Id);
        }
    }
}

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
    public static class AccountCreateEndpoint
    {
        [FunctionName("AccountCreateEndpoint")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, 
            [CosmosDB(databaseName: "MeasurementDB", collectionName: "Accounts", ConnectionStringSetting = "CosmosDBConnection")] IAsyncCollector<Account> documents,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Convert to request object
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var accountDetails = JsonConvert.DeserializeObject<AccountDetails>(requestBody);

            // Map to document object
            if (string.IsNullOrEmpty(accountDetails.AccountName))
                return new BadRequestObjectResult("Account Name is required.");

            var account = new Account(accountDetails);

            // Save
            await documents.AddAsync(account);

            return new OkObjectResult(account.Id);
        }
    }
}

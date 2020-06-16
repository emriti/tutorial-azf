using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;

namespace TutorialAZF.TutorialEvg
{
    public static class PublishEvg1
    {
        [FunctionName("TutorialAZF-PublishEvg1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [EventGrid(TopicEndpointUri = "evg-topic-endpoint", TopicKeySetting = "evg-topic-key")] IAsyncCollector<string> outputEvents,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            await outputEvents.AddAsync(data);

            string responseMessage = "";

            return new OkObjectResult(responseMessage);
        }
    }
}

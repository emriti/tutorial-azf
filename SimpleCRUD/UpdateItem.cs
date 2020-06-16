using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TutorialAZF.Models;

namespace TutorialAZF.SimpleCRUD
{
    public static class UpdateItem
    {
        [FunctionName("UpdateItem")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdateItem/{id}")] HttpRequestMessage req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection",
                Id = "{id}"
            )]ToDoItem updatedItem,
            string id,
            ILogger log)
        {
            var content = req.Content;
            String jsonContent = content.ReadAsStringAsync().Result;
            ToDoItem bodyItem = JsonConvert.DeserializeObject<ToDoItem>(jsonContent);

            if (updatedItem == null)
            {
                return new NotFoundObjectResult($"Id {id} not found!");
            }
            else
            {
                updatedItem.Description = bodyItem.Description;
                updatedItem.isComplete = bodyItem.isComplete;
            }
            return new OkObjectResult("Data updated!");
        }
    }
}

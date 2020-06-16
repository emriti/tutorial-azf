using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TutorialAZF.Models;

namespace TutorialAZF.SimpleCRUD
{
    public static class GetItems
    {
        [FunctionName("GetItems")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "Select i.id, i.description from Items i"
            )] IEnumerable<ToDoItem> toDoItems,
            ILogger log)
        {
            return new OkObjectResult(toDoItems);
            //if (toDoItems == null)
            //{
            //    return new NotFoundObjectResult("No data found!");
            //} else
            //{
            //    var list = (List<ToDoItem>)toDoItems;
            //    if (list.Count > 0)
            //    {
            //        return new OkObjectResult(toDoItems);
            //    }
            //    return new NotFoundObjectResult("No data found!");
            //}
        }

        [FunctionName("GetItemsLinq")]
        public static async Task<IActionResult> GetItemsLinq(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            CosmosClient client = new CosmosClient("AccountEndpoint=https://poc-binus.documents.azure.com:443/;AccountKey=25ywAR0yfMnUZjYHrahmliruTWv0ykFUeEvHusebcNcYnpjrI2xd5AqOK3SATRGKin2VysBUKa4vb2xfaULRgw==;");

            var db = client.GetDatabase("ToDoList");
            var container = db.GetContainer("Items");

            var q = container.GetItemLinqQueryable<Items>();
            var iterator = q
                .Select(p => p)
                .OrderBy(p => p.Id)
                .ToFeedIterator();
            var results = await iterator.ReadNextAsync();

            return new OkObjectResult(results);
        }
    }

}

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using TutorialAZF.Models;

namespace TutorialAZF.SimpleCRUD
{
    public static class InsertItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="newData"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [FunctionName("TutorialAZF-InsertItem")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequestMessage req,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection"
            )] out ToDoItem newData,
            ILogger log)
        {
            var content = req.Content;
            string jsonContent = content.ReadAsStringAsync().Result;
            newData = JsonConvert.DeserializeObject<ToDoItem>(jsonContent);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}

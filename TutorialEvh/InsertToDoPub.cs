using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TutorialAZF.TutorialEvh
{
    public static class InsertToDoPub
    {
        [FunctionName("TutorialAZF-InsertToDoPub")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [EventHub("insert-todo", Connection = "insert-todo-conn")] IAsyncCollector<string> outputEvents,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation($"Receive http request to post data into event hub, body" + requestBody);
            try
            {
                //ToDoItem toDoItem = JsonConvert.DeserializeObject<ToDoItem>(requestBody);
                //await outputEvents.AddAsync(JsonConvert.SerializeObject(toDoItem));
                //return (ActionResult)new OkObjectResult(new ResponseDTO((int) HttpStatusCode.OK, $"Successfully sending item to event hub, item with id {toDoItem.Id}"));

                throw new Exception("Test12345");
            }
            catch (Exception e)
            {
                return (ActionResult)new BadRequestObjectResult(e.Message);
            }
        }
    }
}
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorialAZF.Models;

namespace TutorialAZF.TutorialEvh
{
    public static class InsertToDoSub1
    {
        [FunctionName("TutorialAZF-InsertToDoSub1")]
        public static async Task Run(
            [EventHubTrigger("insert-todo", Connection = "insert-todo-conn", ConsumerGroup = "A")] EventData[] events,
            [CosmosDB(
                databaseName: "ToDoList",
                collectionName: "Items",
                ConnectionStringSetting = "CosmosDBConnection"
            )] DocumentClient client,
            ILogger log)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                    log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");
                    ToDoItem toDoItem = JsonConvert.DeserializeObject<ToDoItem>(messageBody);
                    toDoItem.Id = toDoItem.Id + "_Sub1";
                    await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("ToDoList", "Items"), toDoItem);
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TutorialAZF.Models
{
    public class ToDoItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("isComplete")]
        public bool isComplete { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TutorialAZF.Models
{
    public class Items
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}

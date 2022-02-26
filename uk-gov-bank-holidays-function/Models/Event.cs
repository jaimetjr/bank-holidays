using Newtonsoft.Json;
using System;

namespace uk_gov_bank_holidays_function.Models
{
    public class Event
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }

        [JsonProperty(PropertyName = "bunting")]
        public bool Bunting { get; set; }
    }
}

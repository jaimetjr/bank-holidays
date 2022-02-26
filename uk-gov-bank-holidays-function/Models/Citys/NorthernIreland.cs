using Newtonsoft.Json;
using System.Collections.Generic;

namespace uk_gov_bank_holidays_function.Models.Citys
{
    public class NorthernIreland
    {
        [JsonProperty(PropertyName = "division")]
        public string Division { get; set; }

        [JsonProperty(PropertyName = "events")]
        public List<Event> Events { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using uk_gov_bank_holidays_function.Models.Citys;

namespace uk_gov_bank_holidays_function.Models
{
    public class Root
    {
        [JsonProperty(PropertyName = "england-and-wales")]
        public EnglandAndWales EnglandAndWales { get; set; }

        [JsonProperty(PropertyName = "scotland")]
        public Scotland Scotland { get; set; }

        [JsonProperty(PropertyName = "northern-ireland")]
        public NorthernIreland NorthernIreland { get; set; }

    }
}

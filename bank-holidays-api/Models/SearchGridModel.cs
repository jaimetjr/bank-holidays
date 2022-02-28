using System;
using System.Text.Json.Serialization;

namespace bank_holidays_api.Models
{
    public class SearchGridModel
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("holiday")]
        public string Holiday { get; set; }
        [JsonPropertyName("dayOfWeek")]
        public DayOfWeek DayOfWeek { get; set; }

        [JsonPropertyName("isSearchedDay")]
        public bool IsSearchedDay { get; set; } = false;
    }
}

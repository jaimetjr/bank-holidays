using System;

namespace bank_holidays_api.Models
{
    public class SearchModel
    {
        public DateTime Date { get; set; }
        public string Region { get; set; }
    }
}

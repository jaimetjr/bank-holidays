using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace uk_gov_bank_holidays_function.Models
{
    public class RequestBody
    {
        public string Region { get; set; }
        public DateTime Date { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace uk_gov_bank_holidays_function.Models
{
    public class ReturnedModel
    {
        public DateTime Date { get; set; }
        public string Holiday { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}

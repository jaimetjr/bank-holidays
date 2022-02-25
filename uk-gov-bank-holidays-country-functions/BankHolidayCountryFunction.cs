using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace uk_gov_bank_holidays_country_functions
{
    public static class BankHolidayCountryFunction
    {
        [FunctionName("GetCountrys")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] ILogger log)
        {
            try
            {
                log.LogInformation("Start Process: GetCountrys");
                string url = "https://www.gov.uk/bank-holidays.json";

                using var client = new HttpClient();
                using var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    log.LogInformation("Countrys Get Successfully");
                    var content = await response.Content.ReadAsStringAsync();

                    var jsonObject = JObject.Parse(content);

                    var keys = new List<string>();
                    foreach (var item in jsonObject)
                    {
                        keys.Add(item.Key);
                    }
                    return new OkObjectResult(keys);
                }
                log.LogInformation("Countrys not found");
                return new NotFoundResult();
            }
            catch (Exception ex)
            {
                log.LogError($"Error while getting countrys - {ex.Message} - {ex.InnerException}");
                return new BadRequestResult();
            }
        }
    }
}

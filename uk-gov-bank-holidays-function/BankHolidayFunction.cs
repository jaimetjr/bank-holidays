using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using uk_gov_bank_holidays_function.Models;
using System.Net.Http;

namespace uk_gov_bank_holidays_function
{
    public static class BankHolidayFunction
    {
        [FunctionName("GetBankHolidayByRegionAndDate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Start Method");

            try
            {
                //var body = JsonConvert.DeserializeObject<RequestBody>(await req.ReadAsStringAsync());

                string url = "https://www.gov.uk/bank-holidays.json";

                var client = new HttpClient();
                
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return new OkObjectResult(content);
                }
                return new NotFoundResult();

            }
            catch (Exception ex)
            {
                log.LogError($"{ex.Message} - {ex.InnerException}");
                return new BadRequestResult();
            }
        }
    }
}

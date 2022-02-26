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
using System.Linq;
using System.Collections.Generic;

namespace uk_gov_bank_holidays_function
{
    public static class BankHolidayFunction
    {
        [FunctionName("GetBankHolidayByRegionAndDate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Start Method");

            try
            {
                var requestBody = await req.ReadAsStringAsync();
                if (requestBody == null)
                    return new BadRequestObjectResult("Erro on request body");

                var body = JsonConvert.DeserializeObject<RequestBody>(requestBody);

                string url = Environment.GetEnvironmentVariable("UkGovBankHolidayUrl");

                using var client = new HttpClient();
                using var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    var fields = JsonConvert.DeserializeObject<Root>(content);

                    return new OkObjectResult(SearchContent(body, fields));
                }
                return new NotFoundResult();

            }
            catch (Exception ex)
            {
                log.LogError($"{ex.Message} - {ex.InnerException}");
                return new BadRequestResult();
            }
        }

        private static List<ReturnedModel> SearchContent(RequestBody request, Root root)
        {
            switch (request.Region.ToLower())
            {
                case "scotland":
                    return root.Scotland.Events.Select(x => new ReturnedModel
                    {
                        Date = x.Date,
                        DayOfWeek = x.Date.DayOfWeek,
                        Holiday = x.Title
                    }).ToList();
                case "northern-ireland":
                    return root.NorthernIreland.Events.Select(x => new ReturnedModel
                    {
                        Date = x.Date,
                        DayOfWeek = x.Date.DayOfWeek,
                        Holiday = x.Title
                    }).ToList();
                case "england-and-wales":
                    return root.EnglandAndWales.Events.Select(x => new ReturnedModel
                    {
                        Date = x.Date,
                        DayOfWeek = x.Date.DayOfWeek,
                        Holiday = x.Title
                    }).ToList();
                default:
                    return new List<ReturnedModel>();
            }
        }
    }
}

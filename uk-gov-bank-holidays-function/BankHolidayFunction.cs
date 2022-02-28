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
                    return ScotlandModel(request, root);
                case "northern-ireland":
                    return NorthernIrelandModel(request, root);
                case "england-and-wales":
                    return EnglandAndWalesModel(request, root);
                default:
                    return new List<ReturnedModel>();
            }
        }

        private static List<ReturnedModel> ScotlandModel(RequestBody request, Root root)
        {
            var returnedModels = new List<ReturnedModel>();

            var events = root.Scotland
                                     .Events
                                     .Where(x => x.Date.Year == request.Date.Year)
                                     .OrderBy(x => x.Date)
                                     .ToList();
            int beforeCount = 0;
            int afterCount = 0;

            returnedModels.Add(new ReturnedModel
            {
                Date = request.Date,
                DayOfWeek = request.Date.DayOfWeek,
                Holiday = "[Searched date]",
                IsSearchedDay = true
            });

            for (int i = 0; i < events.Count(); i++)
            {
                if (events[i].Date < request.Date && beforeCount < 2)
                {
                    returnedModels.Add(new ReturnedModel
                    {
                        Date = events[i].Date,
                        DayOfWeek = events[i].Date.DayOfWeek,
                        Holiday = events[i].Title
                    });
                    beforeCount++;
                    continue;
                }

                if (events[i].Date > request.Date && afterCount < 2)
                {
                    returnedModels.Add(new ReturnedModel
                    {
                        Date = events[i].Date,
                        DayOfWeek = events[i].Date.DayOfWeek,
                        Holiday = events[i].Title
                    });
                    afterCount++;
                    continue;
                }
            }
            return returnedModels;
        }

        private static List<ReturnedModel> NorthernIrelandModel(RequestBody request, Root root)
        {
            var returnedModels = new List<ReturnedModel>();

            var events = root.NorthernIreland
                                     .Events
                                     .Where(x => x.Date.Year == request.Date.Year)
                                     .OrderBy(x => x.Date)
                                     .ToList();

            int beforeCount = 0;
            int afterCount = 0;
            returnedModels.Add(new ReturnedModel
            {
                Date = request.Date,
                DayOfWeek = request.Date.DayOfWeek,
                Holiday = "[Searched date]",
                IsSearchedDay = true
            });

            for (int i = 0; i < events.Count(); i++)
            {
                if (events[i].Date < request.Date && beforeCount < 2)
                {
                    returnedModels.Add(new ReturnedModel
                    {
                        Date = events[i].Date,
                        DayOfWeek = events[i].Date.DayOfWeek,
                        Holiday = events[i].Title
                    });
                    beforeCount++;
                    continue;
                }

                if (events[i].Date > request.Date && afterCount < 2)
                {
                    returnedModels.Add(new ReturnedModel
                    {
                        Date = events[i].Date,
                        DayOfWeek = events[i].Date.DayOfWeek,
                        Holiday = events[i].Title
                    });
                    afterCount++;
                    continue;
                }
            }
            return returnedModels;
        }

        private static List<ReturnedModel> EnglandAndWalesModel(RequestBody request, Root root)
        {
            var returnedModels = new List<ReturnedModel>();

            var events = root.EnglandAndWales
                                     .Events
                                     .Where(x => x.Date.Year == request.Date.Year)
                                     .ToList();

            int beforeCount = 0;
            int afterCount = 0;
            returnedModels.Add(new ReturnedModel
            {
                Date = request.Date,
                DayOfWeek = request.Date.DayOfWeek,
                Holiday = "[Searched date]",
                IsSearchedDay = true
            });

            for (int i = 0; i < events.Count(); i++)
            {
                if (events[i].Date < request.Date && beforeCount < 2)
                {
                    returnedModels.Add(new ReturnedModel
                    {
                        Date = events[i].Date,
                        DayOfWeek = events[i].Date.DayOfWeek,
                        Holiday = events[i].Title
                    });
                    beforeCount++;
                    continue;
                }

                if (events[i].Date > request.Date && afterCount < 2)
                {
                    returnedModels.Add(new ReturnedModel
                    {
                        Date = events[i].Date,
                        DayOfWeek = events[i].Date.DayOfWeek,
                        Holiday = events[i].Title
                    });
                    afterCount++;
                    continue;
                }
            }
            return returnedModels;
        }
    }
}

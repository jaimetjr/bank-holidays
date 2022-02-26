using bank_holidays_api.Config;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Options;
using bank_holidays_api.Models;
using System.Text;

namespace bank_holidays_api.Services.BankHolidayService
{
    public class BankHolidayService : IBankHolidayService
    {
        private readonly AppSettings _appSettings;

        public BankHolidayService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<ServiceResponse<List<string>>> GetCountrys()
        {
            var serviceResponse = new ServiceResponse<List<string>>();
            using var client = new HttpClient();
            using var response = await client.GetAsync(_appSettings.AzureFunctionGetCountryUrl);

            if (!response.IsSuccessStatusCode)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error while getting the response";
                return serviceResponse;
            }
            var content = await response.Content.ReadAsStringAsync();

            serviceResponse.Data = JsonSerializer.Deserialize<List<string>>(content);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<SearchGridModel>>> GetBankHolidayByRegionAndDate(SearchModel body)
        {
            var serviceResponse = new ServiceResponse<List<SearchGridModel>>();
            using var client = new HttpClient();
            var stringContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            using var response = await client.PostAsync(_appSettings.AzureFunctionBankHolidayUrl, stringContent);

            if (!response.IsSuccessStatusCode)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Error while getting the response";
                return serviceResponse;
            }

            var content = await response.Content.ReadAsStringAsync();
            serviceResponse.Data = JsonSerializer.Deserialize<List<SearchGridModel>>(content);

            return serviceResponse;
        }
    }
}

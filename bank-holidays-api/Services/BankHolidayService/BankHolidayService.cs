using bank_holidays_api.Config;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Options;

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
            }
            var content = await response.Content.ReadAsStringAsync();

            serviceResponse.Data = JsonSerializer.Deserialize<List<string>>(content);

            return serviceResponse;
        }
    }
}

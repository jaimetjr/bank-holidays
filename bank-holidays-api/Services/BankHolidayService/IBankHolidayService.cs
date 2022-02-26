using bank_holidays_api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bank_holidays_api.Services.BankHolidayService
{
    public interface IBankHolidayService
    {
        Task<ServiceResponse<List<string>>> GetCountrys();
        Task<ServiceResponse<List<SearchGridModel>>> GetBankHolidayByRegionAndDate(SearchModel body);
    }
}

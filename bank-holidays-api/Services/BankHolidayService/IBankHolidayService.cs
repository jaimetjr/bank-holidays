using System.Collections.Generic;
using System.Threading.Tasks;

namespace bank_holidays_api.Services.BankHolidayService
{
    public interface IBankHolidayService
    {
        Task<ServiceResponse<List<string>>> GetCountrys();
    }
}

using bank_holidays_api.Services.BankHolidayService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace bank_holidays_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankHolidayController : ControllerBase
    {
        private readonly IBankHolidayService _service;
        public BankHolidayController(IBankHolidayService service)
        {
            _service = service;
        }

        [HttpGet("GetCountrys")]
        public async Task<IActionResult> GetCountrys()
        {
            var countrys = await _service.GetCountrys();
            return Ok(countrys);
        }
    }
}

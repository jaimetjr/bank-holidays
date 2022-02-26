using bank_holidays_api.Models;
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
            var serviceResponse = await _service.GetCountrys();
            if (serviceResponse.Success)
                return Ok(serviceResponse.Data);
            return BadRequest(serviceResponse.Message);
        }

        [HttpPost("GetBankHolidayByRegionAndDate")]
        public async Task<IActionResult> GetBankHolidayByRegionAndDate([FromBody] SearchModel model)
        {
            var serviceResponse = await _service.GetBankHolidayByRegionAndDate(model);
            if (serviceResponse.Success)
                return Ok(serviceResponse.Data);
            return BadRequest(serviceResponse.Message);
        }
    }
}

using SelfFinanceCommon.Dtos;
using SelfFinanceAPI.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Globalization;
using SelfFinanceCommon;

namespace SelfFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IReportsService _reportsService;
        private readonly IMapper _mapper;

        public ReportController(IReportsService reportsService, IMapper mapper)
        {
            _reportsService = reportsService;
            _mapper = mapper;
        }

        [HttpGet("DailyReport")]
        [ProducesResponseType(200, Type = typeof(ReportDto))]
        public async Task<IActionResult> DailyReport([FromQuery] string dateString)
        {

            if (!DateTime.TryParseExact(dateString, Constants.DateFormat, null, DateTimeStyles.None, out var date))
            {
                return BadRequest("Invalid date format");
            }

            var dailyReport = await _reportsService.DailyReport(date);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(dailyReport);
        }

        [HttpGet("TimePeriodReport")]
        [ProducesResponseType(200, Type = typeof(ReportDto))]
        public async Task<IActionResult> TimePeriodReport([FromQuery] string startDateString, [FromQuery] string endDateString)
        {
            if (!DateTime.TryParseExact(startDateString, Constants.DateFormat, null, DateTimeStyles.None, out var startDate)
                  || !DateTime.TryParseExact(endDateString, Constants.DateFormat, null, DateTimeStyles.None, out var endDate))
            {
                return BadRequest("Invalid date format");
            }

            var report = await _reportsService.PeriodReport(startDate, endDate);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(report);
        }
    }
}


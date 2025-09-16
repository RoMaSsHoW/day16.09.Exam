using Exam.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;

        public ReportsController(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        [HttpGet("top-customers")]
        public async Task<IActionResult> GetTopCustomers([FromQuery] int limit = 5)
        {
            var result = await _reportRepository.GetTopCustomersAsync(limit);
            return Ok(result);
        }

        [HttpGet("bank-income")]
        public async Task<IActionResult> GetBankIncome(
            [FromQuery] DateTime? date_from,
            [FromQuery] DateTime? date_to)
        {
            var result = await _reportRepository.GetBankIncomeAsync(date_from, date_to);
            return Ok(new { Income = result });
        }
    }
}

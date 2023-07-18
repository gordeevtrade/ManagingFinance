using AutoMapper;
using Domain.ServicesInterface;
using FamilyBudjetAPI.Sevices.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudjetAPI.Controllers
{
    //   [Authorize(AuthenticationSchemes = "Google,User")]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IFinanceReportService _financeReportService;
        private readonly IMapper _mapper;
        private readonly IStatisticstService _summaryReportService;

        public ReportController(IFinanceReportService financeReportService, IMapper mapper, IStatisticstService summaryReportService)
        {
            _financeReportService = financeReportService;
            _mapper = mapper;
            _summaryReportService = summaryReportService;
        }

        [HttpGet("get-period-report")]
        public ActionResult<PeriodReportDTO> GetPeriodReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var periodReport = _financeReportService.GetPeriodReport(startDate, endDate);
                var periodReportDto = _mapper.Map<PeriodReportDTO>(periodReport);

                return Ok(periodReportDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
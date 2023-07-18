using AutoMapper;
using Domain.ServicesInterface;
using FamilyBudjetAPI.DTOModels;
using Microsoft.AspNetCore.Mvc;

namespace ManagingFinanceAPI.Controllers
{
    //    [Authorize(AuthenticationSchemes = "Google,User")]
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticstService _summaryReportService;
        private readonly IMapper _mapper;

        public StatisticsController(IMapper mapper, IStatisticstService summaryReportService)
        {
            _mapper = mapper;
            _summaryReportService = summaryReportService;
        }

        [HttpGet("get-statistics-data")]
        public ActionResult<BudgetStatisticsDataDTO> GetStatisticsData()
        {
            try
            {
                var statisticsData = _summaryReportService.GetStatisticsData();
                var statisticsDataDto = _mapper.Map<BudgetStatisticsDataDTO>(statisticsData);
                return Ok(statisticsDataDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
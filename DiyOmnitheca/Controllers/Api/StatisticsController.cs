namespace DiyOmnitheca.Controllers.Api
{
    using DiyOmnitheca.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statistics;

        public StatisticsController(IStatisticsService statistics)
            => this.statistics = statistics;

        [HttpGet]
        public StatisticsServiceModel GetStatistics() 
            => this.statistics.Total();
    }
}

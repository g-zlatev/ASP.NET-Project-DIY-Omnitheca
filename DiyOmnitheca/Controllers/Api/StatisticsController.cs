namespace DiyOmnitheca.Controllers.Api
{
    using System.Linq;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Models.Api.Statistics;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly OmnithecaDbContext data;

        public StatisticsController(OmnithecaDbContext data)
            => this.data = data;

        [HttpGet]
        public StatisticsResponseModel GetStatistics()
        {
            var totalProducts = this.data.Products.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsResponseModel
            {
                TotalProducts = totalProducts,
                TotalUsers = totalUsers,
                TotalRents = 0
            };

        }
    }
}

namespace DiyOmnitheca.Services.Statistics
{
    using DiyOmnitheca.Data;
    using System.Linq;

    public class StatisticsService : IStatisticsService
    {
        private readonly OmnithecaDbContext data;

        public StatisticsService(OmnithecaDbContext data) 
            => this.data = data;

        public StatisticsServiceModel Total()
        {
            var totalProducts = this.data.Products.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalProducts = totalProducts,
                TotalUsers = totalUsers
            };
        }
    }
}

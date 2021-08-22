namespace DiyOmnitheca.Test.Controller.Api
{
    using DiyOmnitheca.Controllers.Api;

    using DiyOmnitheca.Test.Mocks;
    using Xunit;


    public class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics()
        {
            var statisticsController = new StatisticsController(StatisticsServiceMock.Instance);

            var result = statisticsController.GetStatistics();

            Assert.NotNull(result);
            Assert.Equal(5, result.TotalProducts);
            Assert.Equal(4, result.TotalRents);
            Assert.Equal(3, result.TotalUsers);
        }
    }
}

namespace DiyOmnitheca.Test.Mocks
{
    using DiyOmnitheca.Services.Statistics;
    using Moq;

    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance
        {
            get
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalProducts = 5,
                        TotalRents = 4,
                        TotalUsers = 3
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}

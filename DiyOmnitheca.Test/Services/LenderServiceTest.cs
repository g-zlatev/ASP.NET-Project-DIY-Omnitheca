namespace DiyOmnitheca.Test.Services
{
    using DiyOmnitheca.Data.Models;
    using DiyOmnitheca.Services.Lenders;
    using DiyOmnitheca.Test.Mocks;
    using Xunit;


    public class LenderServiceTest
    {
        const string UserId = "TestId";

        [Fact]
        public void IsLenderShouldReturnTrueWhenUserIsLender()
        {
            var lenderService = GetLenderService();

            var result = lenderService.IsLender(UserId);

            Assert.True(result);
        }

        [Fact]
        public void IsLenderShouldReturnFalseWhenUserIsNotLender()
        {
            var lenderService = GetLenderService();

            var result = lenderService.IsLender("WrongId");

            Assert.False(result);
        }

        private static ILenderService GetLenderService()
        {
            var data = DataBaseMock.Instance;

            data.Lenders.Add(new Lender
            {
                UserId = UserId
            });

            data.SaveChanges();

            return new LenderService(data);
        }
    }
}

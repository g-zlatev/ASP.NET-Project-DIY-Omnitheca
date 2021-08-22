namespace DiyOmnitheca.Test.Controller
{
    using DiyOmnitheca.Controllers;
    using DiyOmnitheca.Data.Models;
    using DiyOmnitheca.Models.Home;
    using DiyOmnitheca.Services.Products;
    using DiyOmnitheca.Services.Statistics;
    using DiyOmnitheca.Test.Mocks;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;


    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {

            var data = DataBaseMock.Instance;
            var mapper = MapperMock.Instance;

            var products = Enumerable
                .Range(0, 10)
                .Select(i => new Product());

            data.Products.AddRange(products);
            data.Users.Add(new User());

            data.SaveChanges();

            var productService = new ProductService(data, mapper);
            var statisticsService = new StatisticsService(data);

            var homeController = new HomeController(statisticsService, productService);

            var result = homeController.Index();

            Assert.NotNull(result);
            
            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<IndexViewModel>(model);

            Assert.Equal(3, indexViewModel.Products.Count);
            Assert.Equal(10, indexViewModel.TotalProducts);
            Assert.Equal(1, indexViewModel.TotalUsers);
        }

        [Fact]
        public void ErrorShouldReturnView()
        {
            var homeController = new HomeController(null, null);

            var result = homeController.Error();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}

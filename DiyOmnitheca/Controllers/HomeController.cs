namespace DiyOmnitheca.Controllers
{
    using DiyOmnitheca.Models.Home;
    using DiyOmnitheca.Services.Products;
    using DiyOmnitheca.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly IProductService products;

        public HomeController(
            IStatisticsService statistics,
            IProductService products)
        {
            this.statistics = statistics;
            this.products = products;
        }

        public IActionResult Index()
        {
            var latestProducts = this.products
                .Latest()
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalProducts = totalStatistics.TotalProducts,
                TotalUsers = totalStatistics.TotalUsers,
                Products = latestProducts
            });
        }

       
        public IActionResult Error() => View();
    }
}

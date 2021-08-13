namespace DiyOmnitheca.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Models;
    using DiyOmnitheca.Models.Home;
    using DiyOmnitheca.Services.Statistics;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly IStatisticsService statistics;
        private readonly OmnithecaDbContext data;

        public HomeController(
            IStatisticsService statistics,
            OmnithecaDbContext data)
        {
            this.statistics = statistics;
            this.data = data;
        }

        public IActionResult Index()
        {
            var products = this.data
                .Products
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                })
                .Take(3)
                .ToList();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalProducts = totalStatistics.TotalProducts,
                TotalUsers = totalStatistics.TotalUsers,
                Products = products
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

namespace DiyOmnitheca.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Models;
    using DiyOmnitheca.Models.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly OmnithecaDbContext data;

        public HomeController(OmnithecaDbContext data)
           => this.data = data;


        public IActionResult Index() 
        {
            var totalProducts = this.data.Products.Count();

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

            return View(new IndexViewModel
            {
                TotalProducts = totalProducts,
                Products = products
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

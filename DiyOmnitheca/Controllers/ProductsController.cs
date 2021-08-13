namespace DiyOmnitheca.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Data.Models;
    using DiyOmnitheca.Models.Products;
    using DiyOmnitheca.Infrastructure;
    using DiyOmnitheca.Services.Products;

    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly OmnithecaDbContext data;

        public ProductsController(OmnithecaDbContext data, IProductService products)
        {
            this.data = data;
            this.products = products;
        }


        public IActionResult All([FromQuery] AllProductsQueryModel query)
        {
            var queryResult = this.products.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllProductsQueryModel.ProductsPerPage);

            var productBrands = this.products.AllProductBrands();

            query.Brands = productBrands;
            query.TotalProducts = queryResult.TotalProducts;
            query.Products = queryResult.Products;
                

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsLender())
            {
                return RedirectToAction(nameof(LendersController.Become), "Lenders");
            }

            return View(new AddProductFormModel
            {
                Categories = this.GetProductCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddProductFormModel product)
        {
            var lenderId = this.data
                .Lenders
                .Where(l => l.UserId == this.User.GetId())
                .Select(l => l.Id)
                .FirstOrDefault();

            if (lenderId == 0)
            {
                return RedirectToAction(nameof(LendersController.Become), "Lenders");
            }

            if (!this.data.Categories.Any(c => c.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.GetProductCategories();

                return View(product);
            }

            var productData = new Product
            {
                Brand = product.Brand,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                LendingPrice = (decimal)product.LendingPrice,
                Location = product.Location,
                CategoryId = product.CategoryId,
                LenderId = lenderId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private bool UserIsLender()
            => this.data
                .Lenders
                .Any(l => l.UserId == this.User.GetId());

        private IEnumerable<ProductCategoryViewModel> GetProductCategories()
            => this.data
            .Categories
            .Select(c => new ProductCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }
}

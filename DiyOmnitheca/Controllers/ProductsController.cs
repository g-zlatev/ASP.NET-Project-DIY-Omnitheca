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
    using DiyOmnitheca.Services.Lenders;

    public class ProductsController : Controller
    {
        private readonly OmnithecaDbContext data;
        private readonly IProductService products;
        private readonly ILenderService lenders;

        public ProductsController(OmnithecaDbContext data, IProductService products, ILenderService lenders)
        {
            this.data = data;
            this.products = products;
            this.lenders = lenders;
        }


        public IActionResult All([FromQuery] AllProductsQueryModel query)
        {
            var queryResult = this.products.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllProductsQueryModel.ProductsPerPage);

            var productBrands = this.products.AllBrands();

            query.Brands = productBrands;
            query.TotalProducts = queryResult.TotalProducts;
            query.Products = queryResult.Products;
                

            return View(query);
        }

        [Authorize]
        public IActionResult MyProducts()
        {
            var myProducts = this.products.OwnProducts(this.User.GetId());

            return View(myProducts);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.lenders.IsLender(this.User.GetId()))
            {
                return RedirectToAction(nameof(LendersController.Become), "Lenders");
            }

            return View(new ProductFormModel
            {
                Categories = this.products.AllCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ProductFormModel product)
        {
            var lenderId = this.lenders.GetIdByUser(this.User.GetId());

            if (lenderId == 0)
            {
                return RedirectToAction(nameof(LendersController.Become), "Lenders");
            }

            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllCategories();

                return View(product);
            }

            this.products.Create(
                product.Brand,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.LendingPrice,
                product.Location,
                product.CategoryId,
                lenderId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.lenders.IsLender(userId))
            {
                return RedirectToAction(nameof(LendersController.Become), "Lenders");
            }

            var product = this.products.Details(id);

            if (product.UserId != userId)
            {
                return Unauthorized();
            }

            return View(new ProductFormModel
            {
                Brand = product.Brand,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                LendingPrice = (double)product.LendingPrice,
                Location = product.Location,
                CategoryId = product.CategoryId,
                Categories = this.products.AllCategories()
            });
        }
    }
}

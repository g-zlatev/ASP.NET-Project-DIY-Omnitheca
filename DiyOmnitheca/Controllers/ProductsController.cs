namespace DiyOmnitheca.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Data.Models;
    using DiyOmnitheca.Models.Products;

    public class ProductsController : Controller
    {
        private readonly OmnithecaDbContext data;

        public ProductsController(OmnithecaDbContext data)
            => this.data = data;

        public IActionResult Add() => View(new AddProductFormModel
        {
            Categories = this.GetProductCategories()
        });

        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {
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
                CategoryId = product.CategoryId                
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

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

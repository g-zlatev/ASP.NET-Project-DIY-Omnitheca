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


        public IActionResult All([FromQuery]AllProductsQueryModel query)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Brand))
            {
                productsQuery = productsQuery.Where(p =>
                p.Brand == query.Brand);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                (p.Brand + " " + p.Name).ToLower().Contains(query.SearchTerm.ToLower()) ||
                p.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            productsQuery = query.Sorting switch
            {
                ProductSorting.Price => productsQuery.OrderByDescending(p => p.LendingPrice),
                ProductSorting.BrandAndName => productsQuery.OrderBy(p => p.Brand).ThenBy(p => p.Name),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var products = productsQuery
                .Select(p => new ProductListingViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
                    Category = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    LendingPrice = p.LendingPrice,
                    Location = p.Location
                })
                .ToList();

            var productBrands = this.data
                .Products
                .Select(p => p.Brand)
                .Distinct()
                .ToList();

            query.Brands = productBrands;
            query.Products = products;

            return View(query);
        }


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

            return RedirectToAction(nameof(All));
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

namespace DiyOmnitheca.Controllers.Api
{
    using System.Linq;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Models;
    using DiyOmnitheca.Models.Api.Products;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly OmnithecaDbContext data;

        public ProductsApiController(OmnithecaDbContext data) 
            => this.data = data;

        [HttpGet]
        public ActionResult<AllProductsApiResponseModel> All([FromQuery] AllProductsApiRequestModel query)
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

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((query.CurrentPage - 1) * query.ProductsPerPage)
                .Take(query.ProductsPerPage)
                .Select(p => new ProductResponseModel
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

            return new AllProductsApiResponseModel
            {
                ProductsPerPage = query.ProductsPerPage,
                CurrentPage = query.CurrentPage,
                TotalProducts = totalProducts,
                Products = products
            };

        }
    }
}

namespace DiyOmnitheca.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Models;

    public class ProductService : IProductService
    {
        private readonly OmnithecaDbContext data;

        public ProductService(OmnithecaDbContext data) 
            => this.data = data;

        public ProductQueryServiceModel All(
            string brand,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(brand))
            {
                productsQuery = productsQuery.Where(p =>
                p.Brand == brand);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                (p.Brand + " " + p.Name).ToLower().Contains(searchTerm.ToLower()) ||
                p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            productsQuery = sorting switch
            {
                ProductSorting.Price => productsQuery.OrderByDescending(p => p.LendingPrice),
                ProductSorting.BrandAndName => productsQuery.OrderBy(p => p.Brand).ThenBy(p => p.Name),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new ProductServiceModel
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

            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                Products = products
            };
        }

        public IEnumerable<string> AllProductBrands() 
            => this.data
                .Products
                .Select(p => p.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();
    }
}

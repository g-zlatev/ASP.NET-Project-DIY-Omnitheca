namespace DiyOmnitheca.Services.Products
{
    using System.Collections.Generic;
    using System.Linq;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Data.Models;
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

            var products = GetProducts(productsQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage));


            return new ProductQueryServiceModel
            {
                TotalProducts = totalProducts,
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                Products = products
            };
        }

        public ProductDetailsServiceModel Details(int id)
            => this.data
                .Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    LendingPrice = p.LendingPrice,
                    Location = p.Location,
                    LenderId = p.LenderId,
                    LenderName = p.Lender.Name,
                    UserId = p.Lender.UserId
                })
                .FirstOrDefault();

        public int Create(string brand, string name, string description, string imageUrl, double lendingPrice, string location, int categoryId, int lenderId)
        {
            var productData = new Product
            {
                Brand = brand,
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                LendingPrice = (decimal)lendingPrice,
                Location = location,
                CategoryId = categoryId,
                LenderId = lenderId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return productData.Id;
        }

        public bool Edit(int id, string brand, string name, string description, string imageUrl, double lendingPrice, string location, int categoryId)
        {
            var productData = this.data
                .Products
                .Find(id);

            if (productData == null)
            {
                return false;
            }

            productData.Brand = brand;
            productData.Name = name;
            productData.Description = description;
            productData.ImageUrl = imageUrl;
            productData.LendingPrice = (decimal)lendingPrice;
            productData.Location = location;
            productData.CategoryId = categoryId;

            this.data.SaveChanges();

            return true;
        }

        public IEnumerable<ProductServiceModel> OwnProducts(string userId)
            => GetProducts(this.data
                .Products
                .Where(p => p.Lender.UserId == userId));


        public bool IsByLender(int productId, int lenderId)
            => this.data
                .Products
                .Any(p => p.Id == productId && p.LenderId == lenderId);


        public IEnumerable<string> AllBrands()
            => this.data
                .Products
                .Select(p => p.Brand)
                .Distinct()
                .OrderBy(br => br)
                .ToList();

        public IEnumerable<ProductCategoryServiceModel> AllCategories()
         => this.data
            .Categories
            .Select(c => new ProductCategoryServiceModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        public bool CategoryExists(int categoryId)
            => this.data
                .Categories
                .Any(c => c.Id == categoryId);


        private static IEnumerable<ProductServiceModel> GetProducts(IQueryable<Product> productQuery)
            => productQuery
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Brand = p.Brand,
                    Description = p.Description,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ImageUrl,
                    LendingPrice = p.LendingPrice,
                    Location = p.Location
                })
                .ToList();

    }
}

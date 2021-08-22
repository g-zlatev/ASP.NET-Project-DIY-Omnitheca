namespace DiyOmnitheca.Services.Products
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Data.Models;
    using DiyOmnitheca.Models;

    public class ProductService : IProductService
    {
        private readonly OmnithecaDbContext data;
        private readonly IMapper mapper;

        public ProductService(OmnithecaDbContext data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }


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

        public IEnumerable<LatestProductServiceModel> Latest()
            => this.data
                .Products
                .OrderByDescending(p => p.Id)
                .ProjectTo<LatestProductServiceModel>(this.mapper.ConfigurationProvider)
                .Take(3)
                .ToList();


        public ProductDetailsServiceModel Details(int id)
        {
            var product = this.data
                .Products
                .Where(p => p.Id == id)
                .ProjectTo<ProductDetailsServiceModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault();

            var lenderId = this.data
                .Lenders
                .Where(l => l.Id == product.LenderId)
                .Select(x => x.UserId)
                .FirstOrDefault();

            if (lenderId != null)
            {
                var lenderName = this.data
                .Users
                .Find(lenderId);

                product.LenderName = lenderName.FullName;
            }

            return product;
        }


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

        public bool Lend(int id, DateTime LendUntil, int borrowerId)
        {
            var productData = this.data
                .Products
                .Find(id);

            if (productData == null)
            {
                return false;
            }

            var borrower = this.data.Borrowers.Find(borrowerId);

            productData.BorrowedOnDate = DateTime.UtcNow.ToShortDateString();
            productData.BorrowedUntilDate = LendUntil.ToShortDateString();
            productData.BorrowerId = borrowerId;
            productData.Borrower = borrower;

            borrower.BorrowedProducts.Add(productData);

            //var products = borrower.BorrowedProducts;
            //products.Add(productData);

            this.data.SaveChanges();

            return true;
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


        public IEnumerable<ProductDetailsServiceModel> MyBorrows(string userId)
           => GetBorrowedProducts(this.data
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

        private static IEnumerable<ProductDetailsServiceModel> GetBorrowedProducts(IQueryable<Product> productQuery)
            => productQuery
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
                    BorrowerId = p.BorrowerId,
                    LendFrom = p.BorrowedOnDate,
                    LendUntil = p.BorrowedUntilDate,
                    LenderId = p.LenderId
                })
                .ToList();

    }
}

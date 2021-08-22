namespace DiyOmnitheca.Services.Products
{
    using System.Collections.Generic;
    using DiyOmnitheca.Models;
    using DiyOmnitheca.Models.Products;

    public interface IProductService
    {
        ProductQueryServiceModel All(
            string brand,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage);

        IEnumerable<LatestProductServiceModel> Latest();

        ProductDetailsServiceModel Details(int productId);

        int Create(
                string brand,
                string name,
                string description,
                string imageUrl,
                double lendingPrice,
                string location,
                int categoryId,
                int lenderId);

        bool Edit(
                int productId,
                string brand,
                string name,
                string description,
                string imageUrl,
                double lendingPrice,
                string location,
                int categoryId);

        IEnumerable<ProductServiceModel> OwnProducts(string userId);

        bool IsByLender(int productId, int lenderId);

        IEnumerable<string> AllBrands();

        IEnumerable<ProductCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}

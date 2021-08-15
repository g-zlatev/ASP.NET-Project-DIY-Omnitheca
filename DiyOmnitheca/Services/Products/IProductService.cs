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

        ProductDetailsServiceModel Details(int id);

        int Create(
                string brand,
                string name,
                string description,
                string imageUrl,
                double lendingPrice,
                string location,
                int categoryId,
                int lenderId);

        IEnumerable<ProductServiceModel> OwnProducts(string userId);

        IEnumerable<string> AllBrands();

        IEnumerable<ProductCategoryServiceModel> AllCategories();

        bool CategoryExists(int categoryId);
    }
}

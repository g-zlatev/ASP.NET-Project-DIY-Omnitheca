namespace DiyOmnitheca.Models.Products
{
    using System.Collections.Generic;

    public class AllProductsQueryModel
    {
        public IEnumerable<string> Brands { get; init; }

        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public IEnumerable<ProductListingViewModel> Products { get; init; }
    }
}

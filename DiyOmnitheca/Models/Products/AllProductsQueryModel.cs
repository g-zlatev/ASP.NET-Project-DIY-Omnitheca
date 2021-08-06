namespace DiyOmnitheca.Models.Products
{
    using System.Collections.Generic;

    public class AllProductsQueryModel
    {
        public string Brand { get; init; }

        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}

﻿namespace DiyOmnitheca.Models.Products
{
    using System.Collections.Generic;

    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 3;

        public string Brand { get; init; }

        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalProducts { get; set; }

        public IEnumerable<string> Brands { get; set; }

        public IEnumerable<ProductListingViewModel> Products { get; set; }
    }
}

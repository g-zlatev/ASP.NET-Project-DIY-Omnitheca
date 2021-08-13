namespace DiyOmnitheca.Models.Api.Products
{
    using System.Collections.Generic;


    public class AllProductsApiResponseModel
    {
        public int CurrentPage { get; init; }

        public int ProductsPerPage { get; init; }

        public int TotalProducts { get; init; }

        public IEnumerable<ProductResponseModel> Products { get; init; }
    }
}

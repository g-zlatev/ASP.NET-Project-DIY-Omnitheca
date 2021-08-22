namespace DiyOmnitheca.Models.Home
{
    using DiyOmnitheca.Services.Products;
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int TotalProducts { get; init; }

        public int TotalUsers { get; init; }

        public int TotalRents { get; init; }

        public IList<LatestProductServiceModel> Products { get; init; }
    }
}

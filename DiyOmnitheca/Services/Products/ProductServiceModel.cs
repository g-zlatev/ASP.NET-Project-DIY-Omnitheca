namespace DiyOmnitheca.Services.Products
{

    public class ProductServiceModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Brand { get; init; }

        public string Description { get; init; }

        public string Location { get; init; }

        public decimal LendingPrice { get; init; }

        public string ImageUrl { get; init; }

        public string CategoryName { get; init; }
    }
}

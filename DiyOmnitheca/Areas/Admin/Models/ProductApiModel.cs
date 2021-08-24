namespace DiyOmnitheca.Areas.Admin.Models
{
    public class ProductApiModel
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public string Brand { get; init; }

        public string Location { get; init; }

        public string ImageUrl { get; init; }

        public decimal Price { get; init; }

        public string LendOn { get; init; }

        public string LendUntil { get; init; }
    }
}

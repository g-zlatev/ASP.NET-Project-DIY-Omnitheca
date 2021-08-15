namespace DiyOmnitheca.Services.Products
{
    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public int LenderId { get; init; }

        public string LenderName { get; init; }

        public int CategoryId { get; init; }

        public string UserId { get; init; }
    }
}

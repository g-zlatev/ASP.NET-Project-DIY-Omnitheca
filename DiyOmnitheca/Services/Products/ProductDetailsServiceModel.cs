using System;

namespace DiyOmnitheca.Services.Products
{
    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public int LenderId { get; init; }

        public string LenderName { get; set; }

        public int? BorrowerId { get; set; }

        public string BorrowerName { get; set; }

        public string LendUntil { get; set; }

        public string LendFrom { get; set; }

        public int CategoryId { get; init; }

        public string UserId { get; init; }
    }
}

using System.Collections.Generic;

namespace DiyOmnitheca.Areas.Admin.Models
{
    public class UserApiModel
    {
        public string Id { get; init; }

        public string Email { get; init; }

        public string FullName { get; init; }

        public bool IsLender { get; init; }

        public bool IsBorrower { get; init; }

        public bool HasPaymentInfo { get; init; }

        public IEnumerable<ProductApiModel> OwnProducts { get; set; } = new List<ProductApiModel>();
        public IEnumerable<ProductApiModel> BorrowedProducts { get; set; } = new List<ProductApiModel>();
    }
}

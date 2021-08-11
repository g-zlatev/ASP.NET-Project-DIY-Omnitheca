namespace DiyOmnitheca.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Person;

    public class Borrower
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(PersonNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string  UserId { get; set; }

        public IEnumerable<Product> BorrowedProducts { get; init; } = new List<Product>();
    }
}

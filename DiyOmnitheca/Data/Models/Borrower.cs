namespace DiyOmnitheca.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Person;

    public class Borrower
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(PersonAddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string  UserId { get; set; }

        public List<Product> BorrowedProducts { get; set; } = new List<Product>();
    }
}

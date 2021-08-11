
namespace DiyOmnitheca.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static DataConstants.Product;

    public class Product
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }


        [MaxLength(ProductBrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(ProductLocationMaxLength)]
        public string Location { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal LendingPrice { get; set; }


        public DateTime? BorrowedOnDate { get; set; }


        public DateTime? BorrowedUntilDate { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        //public IEnumerable<Category> Categories { get; init; } = new List<Category>();

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        public int LenderId { get; init; }

        //[Required]
        public Lender Lender { get; init; }  //TODO: change to relevant User class

        public int BorrowerId { get; set; }
        //[Required]
        public Borrower Borrower { get; set; } //TODO: change to relevant User class

    }

}

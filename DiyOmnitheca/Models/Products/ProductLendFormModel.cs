namespace DiyOmnitheca.Models.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DiyOmnitheca.Data.Models;
    using DiyOmnitheca.Services.Products;

    using static Data.DataConstants.Product;

    public class ProductLendFormModel
    {
        [Required]
        [StringLength(ProductNameMaxLength, MinimumLength = ProductDefaultMinLength)]
        public string Name { get; init; }

        [Required]
        [StringLength(ProductBrandMaxLength, MinimumLength = ProductDefaultMinLength)]
        public string Brand { get; init; }

        [Required]
        [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength)]
        public string Description { get; init; }

        [Required]
        [StringLength(ProductLocationMaxLength, MinimumLength = ProductLocationMinLength)]
        public string Location { get; init; }

        [Range(0, ProductRentCostMaxValue)]
        [DataType(DataType.Currency)]
        public double LendingPrice { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; init; }

        public Lender Owner { get; init; }

        public Borrower Borrower { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public DateTime LendUntilDate { get; set; }

        public DateTime LendFromDate { get; set; }

        public IEnumerable<ProductCategoryServiceModel> Categories { get; set; }
    }
}
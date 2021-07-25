namespace DiyOmnitheca.Models.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddProductFormModel
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

        [Range(0.00, ProductRentCostMaxValue)]
        public decimal LendingPrice { get; init; }

        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageUrl { get; init; }

        public string Owner { get; init; }

        public string Borrower { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ProductCategoryViewModel> Categories { get; set; }
    }
}
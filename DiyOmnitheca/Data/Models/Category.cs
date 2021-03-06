namespace DiyOmnitheca.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CategoryMaxName)]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; init; } = new List<Product>();
    }
}

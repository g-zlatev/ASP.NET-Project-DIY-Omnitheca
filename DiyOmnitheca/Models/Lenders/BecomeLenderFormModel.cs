namespace DiyOmnitheca.Models.Lenders
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Person;

    public class BecomeLenderFormModel
    {
        [Required]
        [StringLength(PersonNameMaxLength, MinimumLength = PersonNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}

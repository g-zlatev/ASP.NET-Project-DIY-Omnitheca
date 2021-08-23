namespace DiyOmnitheca.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Identity;

    using static DataConstants.Person;

    public class User : IdentityUser
    {
        [MaxLength(PersonFullNameMaxLength)]
        public string FullName { get; set; }

        public PaymentInfo PaymentInfo { get; set; }
    }
}

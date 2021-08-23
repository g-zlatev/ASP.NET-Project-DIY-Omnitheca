namespace DiyOmnitheca.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Payment;

    public class PaymentInfo
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(BankNameMaxLength)]
        public string BankName { get; set; }

        [Required]
        [MaxLength(IbanMaxLength)]
        public string Iban { get; set; }

        public double Money { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}

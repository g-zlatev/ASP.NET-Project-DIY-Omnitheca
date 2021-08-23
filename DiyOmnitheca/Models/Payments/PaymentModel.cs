using System.ComponentModel.DataAnnotations;

namespace DiyOmnitheca.Models.Payments
{
    public class PaymentModel
    {
        [Display(Name = "New Bank Name")]
        public string BankName { get; set; }

        [Display(Name = "New IBAN")]
        public string Iban { get; set; }

        [Display(Name = "Add/Remove Money")]
        public double Money { get; set; } 
    }
}

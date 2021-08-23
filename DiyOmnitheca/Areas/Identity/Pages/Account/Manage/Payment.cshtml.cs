namespace DiyOmnitheca.Areas.Identity.Pages.Account.Manage
{
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Data.Models;
    using DiyOmnitheca.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using static Data.DataConstants.Payment;

    [Authorize]
    public class PaymentModel : PageModel
    {
        private readonly OmnithecaDbContext data;

        public PaymentModel(
            OmnithecaDbContext data)
        {
            this.data = data;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Bank Name")]
            [StringLength(BankNameMaxLength, MinimumLength = BankNameMinLength)]
            public string BankName { get; set; }

            [Required]
            [Display(Name = "IBAN")]
            [StringLength(IbanMaxLength, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = IbanMinLength)]
            public string Iban { get; set; }

            [Display(Name = "Add Money")]
            public double Money { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public IActionResult OnPost(int id, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var curruserId = this.data
                   .Users
                   .Where(l => l.Id == this.User.GetId())
                   .Select(l => l.Id)
                   .FirstOrDefault();


                var bankInfo = new PaymentInfo
                {
                    BankName = Input.BankName,
                    Iban = Input.Iban,
                    Money = Input.Money,
                    UserId = curruserId
                };

                this.data.PaymentInfos.Add(bankInfo);
                this.data.SaveChanges();

                return LocalRedirect(returnUrl);

            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}



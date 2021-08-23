namespace DiyOmnitheca.Controllers
{
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Infrastructure;
    using DiyOmnitheca.Models.Payments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class PaymentController : Controller
    {
        private readonly OmnithecaDbContext data;

        public PaymentController(OmnithecaDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult Edit()
        {
            var userId = this.User.GetId();

            var payment = this.data
                .PaymentInfos
                .Where(p => p.UserId == userId)
                .FirstOrDefault();

            if (payment == null)
            {
                RedirectToAction("Payment", "Manage", new { area = "Identity/Account" });
            }

            if (payment.UserId != userId)
            {
                return Unauthorized();
            }

            var paymentForm = new PaymentModel
            {
                BankName = payment.BankName,
                Iban = payment.Iban,
                Money = payment.Money
            };

            return View(paymentForm);
        }
    }
}

namespace DiyOmnitheca.Controllers
{
    using System.Linq;
    using DiyOmnitheca.Data;
    using DiyOmnitheca.Data.Models;
    using DiyOmnitheca.Infrastructure;
    using DiyOmnitheca.Models.Lenders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class LendersController : Controller
    {
        private readonly OmnithecaDbContext data;

        public LendersController(OmnithecaDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Become()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeLenderFormModel lender)
        {
            var userId = this.User.GetId();

            var userIsLender = this.data
                .Lenders
                .Any(l => l.UserId == userId);

            if (userIsLender)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(lender);
            }

            var lenderData = new Lender
            {
                Address = lender.Address,
                PhoneNumber = lender.PhoneNumber,
                UserId = userId
            };

            this.data.Lenders.Add(lenderData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Products");
        }
    }
}

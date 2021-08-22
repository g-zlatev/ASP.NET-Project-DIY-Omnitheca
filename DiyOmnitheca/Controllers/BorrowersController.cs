using DiyOmnitheca.Data;
using DiyOmnitheca.Data.Models;
using DiyOmnitheca.Infrastructure;
using DiyOmnitheca.Models.Borrowers;
using DiyOmnitheca.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DiyOmnitheca.Controllers
{

    public class BorrowersController : Controller
    {
        private readonly IProductService products;
        private readonly OmnithecaDbContext data;

        public BorrowersController(OmnithecaDbContext data, IProductService products)
        {
            this.data = data;
            this.products = products;
        }

        [Authorize]
        public IActionResult Become()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeBorrowerFormModel borrower)
        {
            var userId = this.User.GetId();

            var userIsBorrower = this.data
                .Borrowers
                .Any(l => l.UserId == userId);

            if (userIsBorrower)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(borrower);
            }

            var borrowerData = new Borrower
            {
                Address = borrower.Address,
                PhoneNumber = borrower.Address,
                UserId = userId
            };

            this.data.Borrowers.Add(borrowerData);
            this.data.SaveChanges();

            return RedirectToAction("All", "Products");
        }

        [Authorize]
        public IActionResult MyBorrows()
        {
            var myProducts = this.products.MyBorrows(this.User.GetId());

            return View(myProducts);
        }
    }
}

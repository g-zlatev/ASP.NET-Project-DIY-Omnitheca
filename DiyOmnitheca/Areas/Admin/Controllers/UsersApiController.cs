namespace DiyOmnitheca.Areas.Admin.Controllers
{
    using DiyOmnitheca.Areas.Admin.Models;
    using DiyOmnitheca.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("api/users")]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersApiController : ControllerBase
    {

        private readonly OmnithecaDbContext data;

        public UsersApiController(OmnithecaDbContext data)
            => this.data = data;

        [HttpGet]

        public List<UserApiModel> All()
        {
            List<UserApiModel> users = this.data.Users.Select(u => new UserApiModel
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                IsBorrower = this.data.Borrowers.Any(b => b.UserId == u.Id),
                IsLender = this.data.Lenders.Any(l => l.UserId == u.Id),
                HasPaymentInfo = this.data.PaymentInfos.Any(p => p.UserId == u.Id)
            })
                .ToList();

            foreach (var user in users)
            {
                var borrower = this.data.Borrowers.Where(u => u.UserId == user.Id).FirstOrDefault();
                if (borrower != null)
                {
                    user.BorrowedProducts = this.data.Products.Where(p => p.BorrowerId == borrower.Id).Select(p => new ProductApiModel
                    {
                        Id = p.Id,
                        Brand = p.Brand,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        LendOn = p.BorrowedOnDate,
                        LendUntil = p.BorrowedUntilDate,
                        Location = p.Location,
                        Name = p.Name,
                        Price = p.LendingPrice
                    });
                }

                var lender = this.data.Lenders.Where(u => u.UserId == user.Id).FirstOrDefault();
                if (lender != null)
                {
                    user.OwnProducts = this.data.Products.Where(p => p.LenderId == lender.Id).Select(p => new ProductApiModel
                    {
                        Id = p.Id,
                        Brand = p.Brand,
                        Description = p.Description,
                        ImageUrl = p.ImageUrl,
                        LendOn = p.BorrowedOnDate,
                        LendUntil = p.BorrowedUntilDate,
                        Location = p.Location,
                        Name = p.Name,
                        Price = p.LendingPrice
                    });
                }

            }

            return users;
        }
    }
}

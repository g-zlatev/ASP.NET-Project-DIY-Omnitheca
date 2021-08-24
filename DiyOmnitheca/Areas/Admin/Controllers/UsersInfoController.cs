namespace DiyOmnitheca.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UsersInfoController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

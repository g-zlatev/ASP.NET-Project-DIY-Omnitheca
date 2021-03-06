namespace DiyOmnitheca.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using DiyOmnitheca.Models.Products;
    using DiyOmnitheca.Infrastructure;
    using DiyOmnitheca.Services.Products;
    using DiyOmnitheca.Services.Lenders;
    using DiyOmnitheca.Services.Borrowers;
    using AutoMapper;
    using System;

    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly ILenderService lenders;
        private readonly IMapper mapper;
        private readonly IBorrowerService borrowers;

        public ProductsController(IProductService products, ILenderService lenders, IMapper mapper, IBorrowerService borrowers)
        {
            this.products = products;
            this.lenders = lenders;
            this.mapper = mapper;
            this.borrowers = borrowers;
        }


        public IActionResult All([FromQuery] AllProductsQueryModel query)
        {
            var queryResult = this.products.All(
                query.Brand,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                AllProductsQueryModel.ProductsPerPage);

            var productBrands = this.products.AllBrands();

            query.Brands = productBrands;
            query.TotalProducts = queryResult.TotalProducts;
            query.Products = queryResult.Products;
                

            return View(query);
        }

        [Authorize]
        public IActionResult MyProducts()
        {
            var myProducts = this.products.OwnProducts(this.User.GetId());

            return View(myProducts);
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.lenders.IsLender(this.User.GetId()) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(LendersController.Become), "Lenders");
            }

            return View(new ProductFormModel
            {
                Categories = this.products.AllCategories()
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(ProductFormModel product)
        {
            var lenderId = this.lenders.IdByUser(this.User.GetId());

            if (lenderId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(LendersController.Become), "Lenders");
            }

            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllCategories();

                return View(product);
            }

            this.products.Create(
                product.Brand,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.LendingPrice,
                product.Location,
                product.CategoryId,
                lenderId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Lend(int id)
        {
            var product = this.products.Details(id);

            if (!this.borrowers.IsBorrower(this.User.GetId()) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(BorrowersController.Become), "Borrowers");
            }

            return View(product);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Lend(int id, string lendUntil)
        {
            var borrowerId = this.borrowers.IdByUser(this.User.GetId());

            if (borrowerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(BorrowersController.Become), "Borrowers");
            }

            this.products.Lend(id, lendUntil, borrowerId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var product = this.products.Details(id);


            return View(product);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.lenders.IsLender(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(LendersController.Become), "Lenders");
            }

            var lenderId = this.lenders.IdByUser(userId);

            var product = this.products.Details(id);

            if (product.LenderId != lenderId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var productForm = this.mapper.Map<ProductFormModel>(product);

            productForm.Categories = this.products.AllCategories();

            return View(productForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id, ProductFormModel product)
        {
            var lenderId = this.lenders.IdByUser(this.User.GetId());

            if (lenderId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(LendersController.Become), "Lenders");
            }

            if (!this.products.CategoryExists(product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                product.Categories = this.products.AllCategories();

                return View(product);
            }

            if (!this.products.IsByLender(id, lenderId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.products.Edit(
                id,
                product.Brand,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.LendingPrice,
                product.Location,
                product.CategoryId);

            return RedirectToAction(nameof(MyProducts));
        }
    }
}

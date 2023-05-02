using Identity_Session.Business.Abstract;
using Identity_Session.Business.CrossCuttingConcern.Attributes;
using Identity_Session.Core.CrossCuttingConcern.Captche;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net;
using X.PagedList;

namespace Identity_Session.Controllers
{
    [UserLog]
    //[Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        readonly IProductService _productService;
        readonly ICategoryService _categoryService;
        readonly IUserService _userService;
        readonly ICommentService _commentService;
        readonly IPictureService _pictureService;
        readonly ICountryService _countryService;
        readonly IOrderService _orderService;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ISubcategoryService _subcategoryService;
        readonly IRecaptchaValidatorService _captchaService;
        public ProductController(IProductService productService, ICategoryService categoryService, IUserService userService, ICommentService commentService, IPictureService pictureService, ICountryService countryService, IOrderService orderService, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, ISubcategoryService subcategoryService, IRecaptchaValidatorService captchaService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _userService = userService;
            _commentService = commentService;
            _pictureService = pictureService;
            _countryService = countryService;
            _orderService = orderService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _subcategoryService = subcategoryService;
            _captchaService = captchaService;

        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _productService.GetAll();
            return View(result.ToPagedList(page, 25));
        }
        public async Task<IActionResult> UserProduct(string id, int page = 1)
        {
            var result = await _productService.GetAllProductsByUserId(id);
            return View(result.ToPagedList(page, 25));
        }
        public async Task<IActionResult> Category(int id, int page = 1)
        {
            var result = await _productService.GetAllProductsByCateogry(id);
            return View(result.ToPagedList(page, 25));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _productService.GetById(id));
        }
        public async Task<IActionResult> Like(int? id, Product model)
        {
            var result = await _productService.Like(id);
            if (result)
                return RedirectToAction(nameof(Detail), new { id = model.Id });
            else
                return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Dislike(int? id, Product model)
        {
            var result = await _productService.Dislike(id);
            if (result)
                return RedirectToAction(nameof(Detail), new { id = model.Id });
            else
                return RedirectToAction(nameof(Index));
        }
        public IActionResult _HitRead(int? id)
        {
            return PartialView(_productService.HitRead(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string desc, int unitInStock, decimal price, string applicationUserId, int categoryId, int? subcategoryId)
        {
            applicationUserId = Convert.ToString(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            var model = new Product
            {
                Name = name,
                Desc = desc,
                UnitInStock = unitInStock,
                Price = price,
                ApplicationUserId = applicationUserId,
                CategoryId = categoryId,
                SubcategoryId = subcategoryId
            };
            var result = await _productService.Create(model);
            if (result)
            {
                TempData["success"] = "Created";
                return RedirectToAction(nameof(Create));
            }
            else
            {
                TempData["error"] = "Mistake";
                return RedirectToAction(nameof(Create));
            }
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var updateProduct = await _productService.GetById(id);
            if (updateProduct != null)
            {
                return View(updateProduct);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string name, string description, int unitInStock, decimal price, string applicationUserId, int categoryId)
        {
            applicationUserId = Convert.ToString(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            var model = new Product
            {
                Name = name,
                Desc = description,
                UnitInStock = unitInStock,
                Price = price,
                ApplicationUserId = applicationUserId,
                CategoryId = categoryId
            };
            var result = await _productService.Update(model);
            if (result)
            {
                TempData["success"] = "Created";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            else
            {
                TempData["error"] = "Mistake";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
        }

        public async Task<IActionResult> CreateComment(int? id)
        {
            ViewBag.ProductId = await _productService.GetById(id);
            Comment model = new Comment()
            {
                ProductId = id
            };
            return View("CreateComment", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(string nameSurname, string email, string subject, string text, int? productId, string applicationUserId, string token)
        {
            applicationUserId = Convert.ToString(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            if (!_captchaService.IsRecaptchaValid(token))
            {
                //return error message or something
                return BadRequest();
            }
            var model = new Comment()
            {
                NameSurname = nameSurname,
                Email = email,
                Subject = subject,
                Text = text,
                ProductId = productId,
                ApplicationUserId = applicationUserId
            };
            var result = await _commentService.Create(model);
            if (result)
            {
                TempData["success"] = "Created";
                return RedirectToAction(nameof(CreateComment), new { id = model.ProductId });
            }
            else
            {
                TempData["error"] = "Mistake";
                return RedirectToAction(nameof(CreateComment), new { id = model.ProductId });
            }
        }
        public async Task<IActionResult> CreateImage(int? id)
        {
            ViewBag.ProductId = await _productService.GetById(id);
            Picture model = new Picture()
            {
                ProductId = id
            };
            return View("CreateImage", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateImage(string title, int? productId, string applicationUserId, IEnumerable<IFormFile> images)
        {
            applicationUserId = Convert.ToString(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            foreach (var image in images)
            {
                var model = new Picture
                {
                    ProductId = productId,
                    Title = title,
                    ApplicationUserId = applicationUserId
                };
                var path = Path.GetExtension(image.FileName);
                var photoName = Guid.NewGuid() + path;
                var upload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/web/" + photoName);
                var stream = new FileStream(upload, FileMode.Create);
                image.CopyTo(stream);
                model.ImageUrl = photoName;

                var result = _pictureService.CreateSync(model);
                if (result)
                {
                    TempData["success"] = "Created";
                    return RedirectToAction(nameof(CreateImage), new { id = model.ProductId });
                }
                else
                {
                    TempData["error"] = "Mistake";
                    return RedirectToAction(nameof(CreateImage), new { id = model.ProductId });
                }
            }
            TempData["bigError"] = "Big Issue";
            return RedirectToAction(nameof(CreateImage));
        }
        public async Task<IActionResult> CreateOrder(int? id)
        {
            ViewBag.Countries = await _countryService.GetAllCountriesForAdd();
            ViewBag.ProductId = await _productService.GetById(id);
            Order model = new Order()
            {
                ProductId = id
            };
            return View("CreateOrder", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(string city, string province, string address, int quantity, int? productId, int countryId, string applicationUserId)
        {
            applicationUserId = Convert.ToString(_httpContextAccessor.HttpContext.Session.GetString("userId"));
            var model = new Order
            {
                City = city,
                Province = province,
                Address = address,
                Quantity = quantity,
                ProductId = productId,
                CountryId = countryId,
                ApplicationUserId = applicationUserId
            };
            var result = await _orderService.Create(model);
            if (result)
            {
                TempData["success"] = "Created";
                return RedirectToAction(nameof(CreateOrder), new { id = model.ProductId });
            }
            else
            {
                TempData["error"] = "Mistake";
                return RedirectToAction(nameof(CreateOrder), new { id = model.ProductId });
            }
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var deleteProduct = await _productService.GetById(id);
            if (deleteProduct != null)
            {
                var result = await _productService.Delete(deleteProduct);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Active(int id, Product model)
        {
            await _productService.SetActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> DeActive(int id, Product model)
        {
            await _productService.SetDeActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> Deleted(int id, Product model)
        {
            await _productService.SetDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> NotDeleted(int id, Product model)
        {
            await _productService.SetNotDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }


        [HttpPost]
        public async Task<JsonResult> SelectSystem(int? categoryId, string tip)
        {
            var categoryList = await _categoryService.GetAllCategoriesForAdd();
            var subcategoryList = await _subcategoryService.GetAllSubcategoriesByCategoryIdForAdd(categoryId);
            List<SelectListItem> result = new List<SelectListItem>();
            bool isOke = true;
            try
            {
                switch (tip)
                {
                    case "GetCategory":
                        if (categoryList != null)
                        {
                            foreach (var item in categoryList)
                            {
                                result.Add(new SelectListItem
                                {
                                    Text = item.Name,
                                    Value = item.Id.ToString()
                                });
                            }
                        }
                        break;
                    case "GetSubcategory":
                        foreach (var item in subcategoryList)
                        {
                            result.Add(new SelectListItem
                            {
                                Text = item.Name,
                                Value = item.Id.ToString()
                            });
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception)
            {
                isOke = false;
                result = new List<SelectListItem>();
                result.Add(new SelectListItem
                {
                    Text = "Something went wrong",
                    Value = "Default"
                });
            }
            return Json(new { ok = isOke, text = result });
        }

        public JsonResult OnGetRecaptchaVerify(string token)
        {
            return new JsonResult(_captchaService.IsRecaptchaValid(token));
        }
    }
}
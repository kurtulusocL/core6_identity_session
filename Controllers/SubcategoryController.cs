using Identity_Session.Business.Abstract;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Identity_Session.Controllers
{
    public class SubcategoryController : Controller
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly ICategoryService _categoryService;
        public SubcategoryController(ISubcategoryService subcategoryService, ICategoryService categoryService)
        {
            _subcategoryService = subcategoryService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _subcategoryService.GetAll();
            return View(result.ToPagedList(page, 20));
        }
        public async Task<IActionResult> Category(int? id, int page = 1)
        {
            var result = await _subcategoryService.GetAllSubcategoriesByCategoryId(id);
            return View(result.ToPagedList(page, 20));
        }
        public async Task<IActionResult> Create()
        {
           ViewBag.Categories = await _categoryService.GetAllCategoriesForAdd();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subcategory model)
        {
            var result = await _subcategoryService.Create(model);
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
    }
}

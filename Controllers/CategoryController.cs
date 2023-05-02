using Identity_Session.Business.Abstract;
using Identity_Session.Business.CrossCuttingConcern.Attributes;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Identity_Session.Controllers
{
    [ExceptionHandler]
    public class CategoryController : Controller
    {
        readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _categoryService.GetAll();
            return View(result.ToPagedList(page, 15));
        }
        public async Task<IActionResult> kurtulusocL(int page = 1)
        {
            var result = await _categoryService.GetAllCategoriesWithoutParameter();
            return View(result.ToPagedList(page, 25));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _categoryService.GetById(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            try
            {
                var result = await _categoryService.Create(model);
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
            catch (Exception)
            {
                throw new Exception("Hata");
            }
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var updateCategory = await _categoryService.GetById(id);
            if (updateCategory != null)
            {
                return View(updateCategory);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category model)
        {
            var result = await _categoryService.Update(model);
            if (result)
            {
                TempData["success"] = "Updated";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
            else
            {
                TempData["error"] = "Mistake";
                return RedirectToAction(nameof(Edit), new { id = model.Id });
            }
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var deleteCategory = await _categoryService.GetById(id);
            if (deleteCategory != null)
            {
                var result = await _categoryService.Delete(deleteCategory);
                if (result)
                {
                    TempData["success"] = "Deleted";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Mistake";
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Active(int id, Category model)
        {
            await _categoryService.SetActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> DeActive(int id, Category model)
        {
            await _categoryService.SetDeActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> Deleted(int id, Category model)
        {
            await _categoryService.SetDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> NotDeleted(int id, Category model)
        {
            await _categoryService.SetNotDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
    }
}

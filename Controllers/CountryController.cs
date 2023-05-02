using Identity_Session.Business.Abstract;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Identity_Session.Controllers
{
    public class CountryController : Controller
    {
        readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _countryService.GetAll();
            return View(result.ToPagedList(page, 15));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _countryService.GetById(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country model)
        {
            var result = await _countryService.Create(model);
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
            var updateCountry = await _countryService.GetById(id);
            if (updateCountry != null)
            {
                return View(updateCountry);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Country model)
        {
            var result = await _countryService.Update(model);
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
            var deleteCountry = await _countryService.GetById(id);
            if (deleteCountry != null)
            {
                var result = await _countryService.Delete(deleteCountry);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Active(int id, Country model)
        {
            await _countryService.SetActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> DeActive(int id, Country model)
        {
            await _countryService.SetDeActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> Deleted(int id, Country model)
        {
            await _countryService.SetDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> NotDeleted(int id, Country model)
        {
            await _countryService.SetNotDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
    }
}

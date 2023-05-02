using Identity_Session.Business.Abstract;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Identity_Session.Controllers
{
    public class ProfilePhotoController : Controller
    {
        readonly IProfilePhotoService _profilePhotoService;
        public ProfilePhotoController(IProfilePhotoService profilePhotoService)
        {
            _profilePhotoService = profilePhotoService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _profilePhotoService.GetAll();
            return View(result.ToPagedList(page, 24));
        }
        public async Task<IActionResult> UserImage(string id, int page = 1)
        {
            var result = await _profilePhotoService.GetAllProfilePhotoByUserId(id);
            return View(result.ToPagedList(page, 24));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _profilePhotoService.GetById(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var deleteImage = await _profilePhotoService.GetById(id);
            if (deleteImage != null)
            {
                var result = await _profilePhotoService.Delete(deleteImage);
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
        public async Task<IActionResult> Active(int id, ProfilePhoto model)
        {
            await _profilePhotoService.SetActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> DeActive(int id, ProfilePhoto model)
        {
            await _profilePhotoService.SetDeActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> Deleted(int id, ProfilePhoto model)
        {
            await _profilePhotoService.SetDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> NotDeleted(int id, ProfilePhoto model)
        {
            await _profilePhotoService.SetNotDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
    }
}
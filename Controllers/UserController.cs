using Identity_Session.Business.Abstract;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Identity_Session.Controllers
{
    public class UserController : Controller
    {
        readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _userService.GetAll();
            return View(result.ToPagedList(page, 30));
        }
        public async Task<IActionResult> Admin(int page = 1)
        {
            var result = await _userService.GetAllAdmins();
            return View(result.ToPagedList(page, 20));
        }
        public async Task<IActionResult> Member(int page = 1)
        {
            var result = await _userService.GetAllMembers();
            return View(result.ToPagedList(page, 20));
        }
        public async Task<IActionResult> Detail(string id)
        {
            return View(await _userService.GetById(id));
        }
        public async Task<IActionResult> Delete(string id)
        {
            var deleteUser = await _userService.GetById(id);
            if (deleteUser != null)
            {
                var result = await _userService.Delete(deleteUser);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Active(string id, ApplicationUser model)
        {
            await _userService.SetActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> DeActive(string id, ApplicationUser model)
        {
            await _userService.SetDeActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> Deleted(string id, ApplicationUser model)
        {
            await _userService.SetDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> NotDeleted(string id, ApplicationUser model)
        {
            await _userService.SetNotDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
    }
}

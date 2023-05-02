using Identity_Session.Business.Abstract;
using Identity_Session.Core.CrossCuttingConcern.Role.Microsoft;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Identity_Session.Controllers
{
    public class RoleController : Controller
    {
        readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _roleService.GetAll();
            return View(result.ToPagedList(page, 15));
        }
        public async Task<IActionResult> Detail(string id)
        {
            return View(await _roleService.GetById(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationRole model)
        {
            var result = await _roleService.Create(model);
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
        public async Task<IActionResult> Edit(string id)
        {
            var updateRole = await _roleService.GetById(id);
            if (updateRole != null)
            {
                return View(updateRole);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationRole model)
        {
            var result = await _roleService.Update(model);
            if (result)
            {
                TempData["success"] = "Updated";
                return RedirectToAction(nameof(Edit));
            }
            else
            {
                TempData["error"] = "Mistake";
                return RedirectToAction(nameof(Edit));
            }
        }
        public async Task<IActionResult> Delete(string id)
        {
            var deleteRole = await _roleService.GetById(id);
            if (deleteRole != null)
            {
                var result = await _roleService.Delete(deleteRole);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                    return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Active(string id, ApplicationRole model)
        {
            await _roleService.SetActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> DeActive(string id, ApplicationRole model)
        {
            await _roleService.SetDeActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> Deleted(string id, ApplicationRole model)
        {
            await _roleService.SetDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> NotDeleted(string id, ApplicationRole model)
        {
            await _roleService.SetNotDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
    }
}

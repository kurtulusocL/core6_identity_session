using Identity_Session.Business.Abstract;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Session.Controllers
{
    public class AdminController : Controller
    {
        readonly IUserService _userService;
        readonly IHttpContextAccessor _contextAccessor;
        public AdminController(IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;

        }
        public async Task<IActionResult> Index(string id)
        {
            return View(await _userService.GetById(id));
        }
        public IActionResult Detail (string id,ApplicationUser user)
        {
            return View();
        }
    }
}

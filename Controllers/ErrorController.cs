using Microsoft.AspNetCore.Mvc;

namespace Identity_Session.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound(int code=0)
        {
            TempData["404"] = "NotFound";
            return View(code);
        }
    }
}

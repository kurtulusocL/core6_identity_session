using Identity_Session.Business.Abstract;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Identity_Session.Controllers
{
    public class CommentController : Controller
    {
        readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IActionResult _HitRead(int? id)
        {
            return PartialView(_commentService.HitRead(id));
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _commentService.GetAll();
            return View(result.ToPagedList(page, 25));
        }
        public async Task<IActionResult> Product(int? id, int page = 1)
        {
            var result = await _commentService.GetAllCommentsByProduct(id);
            return View(result.ToPagedList(page, 25));
        }
        public async Task<IActionResult> UserComment(string id, int page = 1)
        {
            var result = await _commentService.GetAllCommentsByUser(id);
            return View(result.ToPagedList(page, 25));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _commentService.GetById(id));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var deleteComment = await _commentService.GetById(id);
            if (deleteComment != null)
            {
                var result = await _commentService.Delete(deleteComment);
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
        public async Task<IActionResult> Active(int id, Comment model)
        {
            await _commentService.SetActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> DeActive(int id, Comment model)
        {
            await _commentService.SetDeActive(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> Deleted(int id, Comment model)
        {
            await _commentService.SetDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
        public async Task<IActionResult> NotDeleted(int id, Comment model)
        {
            await _commentService.SetNotDeleted(id);
            return RedirectToAction(nameof(Detail), new { id = model.Id });
        }
    }
}

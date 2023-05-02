using Identity_Session.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Session.ViewComponents
{
    public class CommentHit : ViewComponent
    {
        readonly ICommentService _commentService;
        public CommentHit(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public IViewComponentResult Invoke(int? id)
        {
            return View(_commentService.HitRead(id));
        }
    }
}

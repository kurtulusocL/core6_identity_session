using Identity_Session.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Identity_Session.ViewComponents
{
    public class UserProfileImage : ViewComponent
    {
        readonly IProfilePhotoService _profilePhotoService;
        public UserProfileImage(IProfilePhotoService profilePhotoService)
        {
            _profilePhotoService = profilePhotoService;
        }
        public IViewComponentResult Invoke(string userId)
        {
            return View(_profilePhotoService.GetProfilePhotoByUserId(userId));
        }
    }
}

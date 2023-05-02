using Identity_Session.Core.CrossCuttingConcern.Audit;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity_Session.Business.CrossCuttingConcern.Attributes
{
    public class UserLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ApplicationUser user = new ApplicationUser();
            var request = filterContext.HttpContext.Request;
            Audit audit = new Audit()
            {
                UserName = (request.HttpContext.User.Identity.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                // IPAddress = request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                UserId = user.Id,
                IPAddress = IpAddress.FindUserIp(),
                Browser = request.HttpContext.Request.Headers["User-Agent"].ToString(),
                BrowserVersion = request.HttpContext.Request.Headers["User-Agent-Version"].ToString(),
                Language = request.HttpContext.Request.Headers["Accept-Language"].ToString(),
                AreaAccessed = request.HttpContext.Request.QueryString.ToUriComponent(),
                //Browser = request.HttpContext.Request.Browser.Browser,
                //IsMobile = request.Browser.IsMobileDevice,
                //DeviceModel = request.Browser.MobileDeviceModel,
                //Platform = request.Browser.Platform,
                MacAddress = MacAddress.GetMACAddress(),
                CreatedDate = DateTime.Now
            };

            ApplicationDbContext context = new ApplicationDbContext();
            context.Audits.Add(audit);
            context.SaveChanges();
            base.OnActionExecuting(filterContext);
        }
    }
}

using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Identity_Session.Business.CrossCuttingConcern.Attributes
{
    public class ExceptionHandlerAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionStackTrace = filterContext.Exception.StackTrace,
                    ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                    CreatedDate = DateTime.Now
                };

                ApplicationDbContext context = new ApplicationDbContext();
                context.ExceptionLoggers.Add(logger);
                context.SaveChanges();
                filterContext.ExceptionHandled = true;
            }
        }
    }
}

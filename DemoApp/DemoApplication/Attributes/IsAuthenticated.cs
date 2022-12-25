using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoApplication.Attributes
{
    public class IsAuthenticated : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                RedirectToLogin(context);
            }
        }

        private void RedirectToLogin(ActionExecutingContext filterContext)
        {
            var redirectTarget = new RouteValueDictionary
            {
                {"action", "dashboard"},
                {"controller", "account"}
            };

            filterContext.Result = new RedirectToRouteResult(redirectTarget);
        }
    }
}

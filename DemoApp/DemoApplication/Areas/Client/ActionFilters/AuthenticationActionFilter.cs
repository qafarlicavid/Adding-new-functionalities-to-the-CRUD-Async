using DemoApplication.Controllers;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoApplication.Areas.Client.ActionFilters
{
    //public class AuthenticationActionFilter : IAsyncActionFilter
    //{
    //    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    {

    //        //Get data




    //        //Set data



    //        await next();
    //    }
    //}

    public class AuthenticationActionFilter : IActionFilter
    {
        private readonly IUserService _userService;

        public AuthenticationActionFilter(IUserService userService)
        {
            _userService = userService;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_userService.IsAuthenticated)
            {
                var controller = (AuthenticationController)context.Controller;
                context.Result = controller.RedirectToRoute("client-account-dashboard");
            }
        }
    }
}

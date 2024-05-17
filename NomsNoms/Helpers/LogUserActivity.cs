using Microsoft.AspNetCore.Mvc.Filters;
using NomsNoms.Extensions;
using NomsNoms.Interfaces;

namespace NomsNoms.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();


            //This is not nessessary because this is called inside a method that has [Authorized] attribute on it
            //However, if someone magically can get access to this method, we can be clear about that.
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();

            var uow = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            await uow.UpdateUserLastActive(userId);
        }
    }
}

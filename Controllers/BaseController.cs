using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nhom12_EWallet.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Session.GetInt32("UserId");
            var role = context.HttpContext.Session.GetInt32("UserRole");

            if (!userId.HasValue || userId.Value == 0)
            {
                TempData["ErrorMessage"] = "Vui lòng đăng nhập để thực hiện tiếp!";
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "User",
                    action = "Login",
                    area = ""
                }));
            }

            base.OnActionExecuting(context);
        }
    }

}

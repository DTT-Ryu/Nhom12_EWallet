using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nhom12_EWallet.Areas.Admin.Controllers
{
    public class BaseAdminController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var role = HttpContext.Session.GetInt32("UserRole");

            Console.WriteLine($"Session kiểm tra: UserId={userId}, UserRole={role}");

            if (userId == null || userId == 0)
            {
                TempData["ErrorMessage"] = "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại!";
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "User",
                    action = "Login",
                    area = ""
                }));
            }
            else if (role != 1) // Chỉ cho phép Admin (role = 1)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập vào chức năng này, vui lòng đăng nhập lại!";
                context.Result = new RedirectToActionResult("Logout", "User", new { area = "" });
            }

            base.OnActionExecuting(context);
        }
    }
}

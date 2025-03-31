using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Respositories;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.GetUserByPhoneNumber(model.PhoneNumber);
            if (user == null || user.SPasswordHash != model.Password)
            {
                ModelState.AddModelError("", "Số điện thoại hoặc mật khẩu không đúng.");
                return View(model);
            }

            // Lưu UserId và Role vào Session
            HttpContext.Session.SetInt32("UserId", user.IUserIdPk);
            HttpContext.Session.SetString("UserName", user.SFullName);
            HttpContext.Session.SetInt32("UserRole", user.IRoleIdFk);

            // Điều hướng theo quyền
            if (user.IRoleIdFk == 1)
            {
                return RedirectToAction("Index", "BankManagement", new { area = "Admin" });
            }
            else if (user.IRoleIdFk == 2)
            {
                return RedirectToAction("Profile", "User");
            }

            // Nếu không thuộc quyền nào, quay về trang chính
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "User");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}

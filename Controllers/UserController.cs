using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Models;
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

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.GetUserByPhoneNumber(model.PhoneNumber);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.SPasswordHash))
            {
                ModelState.AddModelError("", "Số điện thoại hoặc mật khẩu không đúng.");
                return View(model);
            }

            if (user.SStatus == "blocked")
            {
                ModelState.AddModelError("", "Tài khoản đã bị khóa!");
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Lỗi: " + error.ErrorMessage); // Hoặc log vào file hoặc database
                }
                return View(model);
            }

            bool hasError = false;
            //check sđt
            var existPhoneNumber = _userRepository.GetUserByPhoneNumber(model.PhoneNumber);
            if (existPhoneNumber != null)
            {
                ModelState.AddModelError("PhoneNumber", "Số điện thoại đã tồn tại.");
                hasError = true;
            }

            //check cccd
            var existCCCD = _userRepository.GetUserByCCCD(model.CCCD);
            if (existCCCD != null)
            {
                ModelState.AddModelError("CCCD", "Căn cước công dân đã tồn tại.");
                hasError = true;
            }
            
            //check email
            var existEmail = _userRepository.GetUserByEmail(model.Email);
            if(existEmail != null)
            {
                ModelState.AddModelError("Email", "Email đã tồn tại.");
                hasError = true;
            }

            if (hasError)
            {
                return View(model);
            }

            //mã hóa 
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            string hashedPin = BCrypt.Net.BCrypt.HashPassword(model.PinCode);

            var newUser = new TblUser
            {
                SFullName = model.FullName,
                SPhoneNumber = model.PhoneNumber,
                SCccd = model.CCCD,
                DBirthDate = DateOnly.FromDateTime(model.BirthDate),
                SEmail = model.Email,
                FBalance = 0,
                SPasswordHash = hashedPassword,
                SPinCode = hashedPin,
                IRoleIdFk = 2,
                DCreatedAt = DateTime.Now,
                DUpdatedAt = null,
            };

            _userRepository.AddUser(newUser);
            return RedirectToAction("Login", "User");

        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}

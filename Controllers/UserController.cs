using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;
using Nhom12_EWallet.Service.Interfaces;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var user = await _userService.Login(model.PhoneNumber, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Số điện thoại hoặc mật khẩu không đúng.");
                    return View(model);
                }

                _userService.SetUserSession(HttpContext, user);

                return user.IRoleIdFk switch
                {
                    1 => RedirectToAction("Index", "UserManagement", new { area = "Admin" }),
                    2 => RedirectToAction("Profile", "User"),
                    _ => RedirectToAction("Index", "Home")
                };
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout(HttpContext);
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.Register(model);
            if(!result.Success)
            {
                foreach(var e in result.Error)
                {
                    ModelState.AddModelError(e.Key, e.Value);
                }
                return View(model);
            }

            return RedirectToAction("Login", "User");

        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}




//var user = await _userRepository.GetUserByPhoneNumber(model.PhoneNumber);
//if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.SPasswordHash))
//{
//    ModelState.AddModelError("", "Số điện thoại hoặc mật khẩu không đúng.");
//    return View(model);
//}

//if (user.SStatus == "blocked")
//{
//    ModelState.AddModelError("", "Tài khoản đã bị khóa!");
//    return View(model);
//}

//// Lưu UserId và Role vào Session
//HttpContext.Session.SetInt32("UserId", user.IUserIdPk);
//HttpContext.Session.SetString("UserName", user.SFullName);
//HttpContext.Session.SetInt32("UserRole", user.IRoleIdFk);

//// Điều hướng theo quyền
//if (user.IRoleIdFk == 1)
//{
//    return RedirectToAction("Index", "UserManagement", new { area = "Admin" });
//}
//else if (user.IRoleIdFk == 2)
//{
//    return RedirectToAction("Profile", "User");
//}

//// Nếu không thuộc quyền nào, quay về trang chính
//return RedirectToAction("Index", "Home");




//public async Task<IActionResult> Register(RegisterVM model)
//{
//if (!ModelState.IsValid)
//{
//    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
//    {
//        Console.WriteLine("Lỗi: " + error.ErrorMessage); // Hoặc log vào file hoặc database
//    }
//    return View(model);
//}

//bool hasError = false;
////check sđt
//var existPhoneNumber = await _userRepository.GetUserByPhoneNumber(model.PhoneNumber);
//if (existPhoneNumber != null)
//{
//    ModelState.AddModelError("PhoneNumber", "Số điện thoại đã tồn tại.");
//    hasError = true;
//}

////check cccd
//var existCCCD = await _userRepository.GetUserByCCCD(model.CCCD);
//if (existCCCD != null)
//{
//    ModelState.AddModelError("CCCD", "Căn cước công dân đã tồn tại.");
//    hasError = true;
//}

////check email
//var existEmail = await _userRepository.GetUserByEmail(model.Email);
//if(existEmail != null)
//{
//    ModelState.AddModelError("Email", "Email đã tồn tại.");
//    hasError = true;
//}

//if (hasError)
//{
//    return View(model);
//}

////mã hóa 
//string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
//string hashedPin = BCrypt.Net.BCrypt.HashPassword(model.PinCode);

//var newUser = new TblUser
//{
//    SFullName = model.FullName,
//    SPhoneNumber = model.PhoneNumber,
//    SCccd = model.CCCD,
//    DBirthDate = DateOnly.FromDateTime(model.BirthDate),
//    SEmail = model.Email,
//    FBalance = 0,
//    SPasswordHash = hashedPassword,
//    SPinCode = hashedPin,
//    IRoleIdFk = 2,
//    DCreatedAt = DateTime.Now,
//    DUpdatedAt = null,
//};

//await _userRepository.Add(newUser);
//return RedirectToAction("Login", "User");

using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Models;
using Nhom12_EWallet.Respositories.Interfaces;
using Nhom12_EWallet.Service.Interfaces;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Controllers
{
    public class UserController : Controller
    {
        //private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        //[HttpGet("/login")]
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
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {

                    return user.IRoleIdFk switch
                    {
                        1 => RedirectToAction("Index", "UserManagement", new { area = "Admin" }),
                        2 => RedirectToAction("Profile", "User"),
                        _ => RedirectToAction("Index", "Home")
                    };
                }
                return View();
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

        [HttpGet("/register")]
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

        [HttpGet("/profile")]
        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            Console.WriteLine($"Session kiểm tra trong Profile: UserId={userId}");

            if (!userId.HasValue || userId.Value == 0)
            {
                TempData["ErrorMessage"] = "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại!";
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpGet("/get-user-session")]
        public async Task<IActionResult> GetUserFromSession()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetInt32("UserRole");

            return new JsonResult(new { userName, userId, userRole });
        }

        [HttpGet("/get-user/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByID(id);
            if(user == null)
            {
                return NotFound(new { status = 404, message = "Người dùng không tồn tại" });
            }
            return Ok(user);
        }

        [HttpGet("/get-user-email/{email}")]
        public async Task<IActionResult> ValidateEmail(string email)
        {
            var u = await _userService.GetUserByEmail(email);
            if (u == false ) return new JsonResult(new { success = false, message = "Email đã tồn tại" });
            return new JsonResult(new { success = true});
        }

        [HttpPatch("/update-user-infor/{id}")]
        public async Task<IActionResult> UpdateUserInfor(int id, [FromBody] UserManagementVM vm)
        {
            Console.WriteLine("iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii");
            Console.WriteLine($"Received data: id = {id}, fullName = {vm?.fullName}, email = {vm?.email}, birthDate = {vm?.birthDate}");

            var result = await _userService.UpdateUserInfor(id, vm);
            if(result == null) 
                return Json(new { success = false, message = "Cập nhật thông tin thất bại!" });
            return Json(new { success = true, message ="Cập nhật thông tin thành công!", data = result });
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

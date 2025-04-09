using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Controllers;
using Nhom12_EWallet.Service.Interfaces;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class UserManagementController : BaseAdminController
    {
        private readonly IUserService _userService;
        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/account-management")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            return View(users);
        }
        [HttpGet]        
        
        public async Task<IActionResult> GetUserByID(int id)
        {
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            var model = await _userService.GetUserByID(id);
            if (model == null)
            {
                return Json(new { success = false });
            }
            if(currentUserId == id)
            {
                return Json(new { success = true, model = model, self = true, currentId = currentUserId });
            }

            return Json(new { success = true, model = model, self = false });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _userService.GetListRole();
            return Json(roles);
        }

        //cập nhật quyền
        [HttpPost]
        public async Task<IActionResult> EditRole(int id, byte roleId)
        {
            var user = await _userService.GetUserByID(id);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng!";
                return RedirectToAction("Index");
            }

            // Cập nhật quyền người dùng
            bool result = await _userService.UpdateUserRole(id, roleId);

            if (result)
            {
                TempData["UserName"] = user.fullName;  // Lấy fullName từ database
                TempData["SuccessMessage"] = $"Cập nhật quyền cho người dùng {user.fullName} thành công!";
            }
            else
            {
                TempData["UserName"] = user.fullName;
                TempData["ErrorMessage"] = $"Cập nhật quyền cho người dùng {user.fullName} thất bại!";
            }

            return RedirectToAction("Index");
        }

        //cập nhật trạng thái
        [HttpPost]
        public async Task<IActionResult> EditStatus(int id)
        {
            var user = await _userService.GetUserByID(id);
            if(user == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy người dùng!";
                return RedirectToAction("Index");
            }

            bool result = await _userService.UpdateUserStatus(id);
            if (result)
            {
                if(user.status == "active")
                {
                    TempData["UserName"] = user.fullName;
                    TempData["SuccessMessage"] = $"Khóa tài khoản người dùng {user.fullName} thành công!";
                }
                else
                {
                    TempData["UserName"] = user.fullName;
                    TempData["SuccessMessage"] = $"Mở khóa tài khoản người dùng {user.fullName} thành công!";
                }
            }
            else
            {
                if (user.status == "active")
                {
                    TempData["UserName"] = user.fullName;
                    TempData["ErrorMessage"] = $"Khóa tài khoản người dùng {user.fullName} thất bại!";
                }
                else
                {
                    TempData["UserName"] = user.fullName;
                    TempData["ErrorMessage"] = $"Mở khóa tài khoản người dùng {user.fullName} thất bại!";
                }
            }
            return RedirectToAction("Index");
        }



        //cập nhật quyền với ajax
        //[HttpPost]
        //public async Task<IActionResult> Edit(UserManagementVM model)
        //{
        //    bool result = await _userService.UpdateUserRole(model.id, model.roleId, model.fullName);

        //    if (result)
        //    {
        //        return Json(new { success = true, message = "Cập nhật thành công" });
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Cập nhật thất bại" });
        //    }
        //}

    
    }
}

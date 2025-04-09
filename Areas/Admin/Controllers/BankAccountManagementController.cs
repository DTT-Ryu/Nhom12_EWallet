using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Controllers;
using Nhom12_EWallet.Service.Interfaces;
using Nhom12_EWallet.ViewModels;

namespace Nhom12_EWallet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BankAccountManagementController : BaseAdminController
    {
        private readonly IBankAccountService _bankAccountService;
        public BankAccountManagementController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpGet("/bank-account-management")]
        public async Task<IActionResult> Index()
        {
            var acc = await _bankAccountService.GetAllBankAccount();
            return View(acc);
        }

        [HttpGet]
        public async Task<IActionResult> GetListUser()
        {
            var users = await _bankAccountService.GetListUser();
            return Json(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetListBank()
        {
            var banks = await _bankAccountService.GetListBank();
            return Json(banks);
        }

        [HttpGet]
        public async Task<IActionResult> GetBankAccountByID(int id)
        {
            var acc = await _bankAccountService.GetBankAccountByID(id);
            if(acc != null){
                return Json(new { success = true, acc = acc });
            }else {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetBankAccountByNumber(string n)
        {
            var acc = await _bankAccountService.GetBankAccountByNumber(n);
            if (acc != null)
            {
                return Json(new { success = true, acc = acc });
            }
            else
            {
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BankAccountManagementVM model)
        {
            var result = await _bankAccountService.UpdateBankAccount(model);
            if (result)
            {
                TempData["SuccessMessage"] = "Cập nhật tài khoản ngân hàng thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Cập nhật tài khoản ngân hàng thất bại!";
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Lock(int AccountId)
        {
            var a = await _bankAccountService.GetBankAccountByID(AccountId);
            if(a == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy tài khoản ngân hàng!";
                return RedirectToAction("Index");
            }

            var result = await _bankAccountService.UpdateBankAccountStatus(AccountId);
            if (result)
            {
                if(a.Status == "active")
                {
                    TempData["SuccessMessage"] = "Khóa tài khoản ngân hàng thành công!";
                }
                else
                {
                    TempData["SuccessMessage"] = "Mở khóa tài khoản ngân hàng thành công!";
                }
            }
            else
            {
                if (a.Status == "active")
                {
                    TempData["SuccessMessage"] = "Khóa tài khoản ngân hàng thất bại!";
                }
                else
                {
                    TempData["SuccessMessage"] = "Mở khóa tài khoản ngân hàng thất bại!";
                }
            }
            return RedirectToAction("Index");
        }
    }
}

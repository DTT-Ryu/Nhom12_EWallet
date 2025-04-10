using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Models;
using Nhom12_EWallet.Service.Interfaces;

namespace Nhom12_EWallet.Controllers
{
    public class BankAccountController : BaseController
    {
        private IBankAccountService _bankAccountService;
        public BankAccountController (IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }


        [HttpGet("/add-new-bannk-account")]
        public async Task<IActionResult> AddNewBankAccount()
        {
            return View();
        }
        [HttpGet("/getAllBanks")]
        public async Task<IActionResult> GetAllbank()
        {
            var banks = await _bankAccountService.GetListBank();
            return Json(banks);
        }

        [HttpPost("/getNumber")]
        public async Task<IActionResult> GetBankAccByNumber(string n)
        {
            var acc = await _bankAccountService.GetBankAccountByNumber(n);
            if (acc)
            {
                return Json(new { success = true});
            }
            else
            {
                return Json(new { success = false });
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddBankAccount(TblBankAccount model)
        {
            try
            {
                // Lấy userId từ Session
                var userId = HttpContext.Session.GetInt32("UserId");
                // Gọi service để thêm tài khoản ngân hàng
                await _bankAccountService.AddBankAcoount(userId.Value, model);
                TempData["SuccessMessage"] = "Thêm tài khoản ngân hàng thành công!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.InnerException?.Message;
            }

            return RedirectToAction("Deposit", "Transaction");
        }

        public async Task<IActionResult> CreateBankAccount()
        {
            return View();
        }
    }
}

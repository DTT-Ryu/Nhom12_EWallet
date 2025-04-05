using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Service.Interfaces;
using static Nhom12_EWallet.Service.Interfaces.ITransactionService;
using static Nhom12_EWallet.ViewModels.TransactionViewModel;

namespace Nhom12_EWallet.Controllers
{
    public class TransactionController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Deposit()
        {
            return View();
        }

        //private readonly ITransactionService _walletService;

        //public TransactionController(ITransactionService walletService)
        //{
        //    _walletService = walletService;
        //}

        //private int? GetCurrentUserId()
        //{
        //    return HttpContext.Session.GetInt32("UserId");
        //}

        //private IActionResult RedirectIfNotLoggedIn()
        //{
        //    if (!GetCurrentUserId().HasValue)
        //    {
        //        return RedirectToAction("Login", "User");
        //    }
        //    return null;
        //}

        //[HttpGet]
        //public async Task<IActionResult> Deposit()
        //{
        //    var userId = GetCurrentUserId();
        //    if (!userId.HasValue)
        //    {
        //        return RedirectToAction("Login", "User");
        //    }

        //    var bankAccounts = await _walletService.GetUserBankAccountsAsync(userId.Value);
        //    ViewBag.HasBankAccounts = bankAccounts.Any();
        //    return View(bankAccounts);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Deposit(DepositVM model)
        //{
        //    var userId = GetCurrentUserId();
        //    if (!userId.HasValue)
        //    {
        //        return Json(new { success = false, message = "Vui lòng đăng nhập." });
        //    }

        //    var (success, errorMessage) = await _walletService.DepositAsync(userId.Value, model);
        //    if (!success)
        //    {
        //        return Json(new { success = false, message = errorMessage });
        //    }

        //    return Json(new { success = true, redirectUrl = Url.Action("TransactionHistory", "Transaction") });
        //}

        //[HttpGet]
        //public async Task<IActionResult> AddBankAccount()
        //{
        //    var userId = GetCurrentUserId();
        //    if (!userId.HasValue)
        //    {
        //        return RedirectToAction("Login", "User");
        //    }

        //    var banks = await _walletService.GetAllBanksAsync();
        //    return View(banks);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddBankAccount(AddBankAccountVM model)
        //{
        //    var userId = GetCurrentUserId();
        //    if (!userId.HasValue)
        //    {
        //        return Json(new { success = false, message = "Vui lòng đăng nhập." });
        //    }

        //    var (success, errorMessage) = await _walletService.AddBankAccountAsync(userId.Value, model);
        //    if (!success)
        //    {
        //        return Json(new { success = false, message = errorMessage });
        //    }

        //    return Json(new { success = true, redirectUrl = Url.Action("Deposit", "Transaction") });
        //}
    }
}

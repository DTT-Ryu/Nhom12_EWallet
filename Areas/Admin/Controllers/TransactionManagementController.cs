using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Controllers;
using Nhom12_EWallet.Service;
using Nhom12_EWallet.Service.Interfaces;

namespace Nhom12_EWallet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransactionManagementController : BaseAdminController
    {
        private readonly ITransactionService _transactionService;
        public TransactionManagementController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllTransaction();
            return View(transactions);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionByID(int id)
        {
            var t = await _transactionService.GetTransactionByID(id);
            if (t == null)
            {
                return Json(new { success = false });
            }
            else
            {
                return Json(new { success = true, trans = t });
            }
        }

        public async Task<IActionResult> Delete(int TransactionId)
        {
            var result = await _transactionService.DeleteTransaction(TransactionId);
            if (result)
            {
                TempData["SuccessMessage"] = "Xóa giao dịch thành công!";
            }
            else
            {
                TempData["SuccessMessage"] = "Xóa giao dịch thất bại!";
            }
            return RedirectToAction("Index");
        }
    }
}

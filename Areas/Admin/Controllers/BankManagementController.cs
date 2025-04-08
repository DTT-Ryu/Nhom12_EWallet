using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Models;
using Nhom12_EWallet.Service;
using Nhom12_EWallet.Service.Interfaces;

namespace Nhom12_EWallet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BankManagementController : Controller
    {
        private readonly IBankService _bankService;

        public BankManagementController(IBankService bankService)
        {
            _bankService = bankService;
        }


        public async Task<IActionResult> Index()
        {
            var banks = await _bankService.GetAllBank();
            return View(banks);
        }

        [HttpPost]
        public async Task<IActionResult> ValidateByID(string id)
        {
            var bank = await _bankService.GetBankByID(id);
            if (bank != null)
            {
                return Json(new { success = false, message = "Mã ngân hàng đã tồn tai!" });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> ValidateByName(string name)
        {
            var bank = await _bankService.GetBankByName(name);
            if (bank != null)
            {
                return Json(new { success = false, message = "Tên ngân hàng đã tồn tai!" });

            }
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> GetBankByID(string id)
        {
            var bank = await _bankService.GetBankByID(id);
            if (bank != null)
            {
                return Json(new { success = true, bank = bank });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Create(TblBank model, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                await _bankService.CreateBank(model, ImageFile);
                TempData["SuccessMessage"] = "Thêm ngân hàng mới thành công!";
            }
            else
            {
                string s = "";
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        s += $"Lỗi tại {key}: {error.ErrorMessage} ";
                    }
                }
                TempData["ErrorMessage"] = "Thêm mới ngân hàng thất bại! Vui lòng kiểm tra lại dữ liệu. " + s;
            }
            return RedirectToAction("Index", "BankManagement");

        }

        [HttpPost]
        public async Task<IActionResult> EditInfor(TblBank model, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                await _bankService.UpdateBank(model, ImageFile);
                TempData["SuccessMessage"] = "Cập nhật ngân hàng thành công!";
            }
            else
            {
                string s = "";
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        s += $"Lỗi tại {key}: {error.ErrorMessage} ";
                    }
                }
                TempData["ErrorMessage"] = "Cập nhật ngân hàng thất bại! Vui lòng kiểm tra lại dữ liệu. " + s;
            }
            return RedirectToAction("Index", "BankManagement");
        }

        [HttpPost]
        public async Task<IActionResult> Lock(string SBankIdPk)
        {
            var bank = await _bankService.GetBankByID(SBankIdPk);
            if (bank == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy ngân hàng!";
                return RedirectToAction("Index");
            }
            bool result = await _bankService.UpdateBankDeleted(SBankIdPk);
            if (result)
            {
                if (bank.Deleted) TempData["SuccessMessage"] = "Mở khóa ngân hàng thành công!";
                else TempData["SuccessMessage"] = "Khóa ngân hàng thành công!";
            }
            else
            {
                if (bank.Deleted) TempData["SuccessMessage"] = "Mở khóa ngân hàng thất bại!";
                else TempData["SuccessMessage"] = "Khóa ngân hàng thất bại!";
            }

            return RedirectToAction("Index", "BankManagement");
        }
    }
}

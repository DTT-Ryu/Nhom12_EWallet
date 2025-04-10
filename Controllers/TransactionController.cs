using Microsoft.AspNetCore.Mvc;
using Nhom12_EWallet.Service.Interfaces;
using static Nhom12_EWallet.Service.Interfaces.ITransactionService;
//using static Nhom12_EWallet.ViewModels.TransactionViewModel;

namespace Nhom12_EWallet.Controllers
{
    public class TransactionController : BaseController
    {
        [HttpGet("/deposit")]
        public async Task<IActionResult> Deposit()
        {
            return View();
        }

        [HttpGet("/withdraw")]
        public async Task<IActionResult> Withdraw()
        {
            return View();
        }

        [HttpGet("/transfer")]
        public async Task<IActionResult> Transfer()
        {
            return View();
        }

        [HttpGet("/transaction-history")]
        public async Task<IActionResult> TransactionHistory()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Nhom12_EWallet.Controllers
{
    public class BankAccountController : BaseController
    {
        public IActionResult CreateBankAccount()
        {
            return View();
        }
    }
}

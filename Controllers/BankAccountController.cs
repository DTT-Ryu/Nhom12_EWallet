using Microsoft.AspNetCore.Mvc;

namespace Nhom12_EWallet.Controllers
{
    public class BankAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

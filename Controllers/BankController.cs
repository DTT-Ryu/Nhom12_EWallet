using Microsoft.AspNetCore.Mvc;

namespace Nhom12_EWallet.Controllers
{
    public class BankController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

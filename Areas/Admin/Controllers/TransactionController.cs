using Microsoft.AspNetCore.Mvc;

namespace Nhom12_EWallet.Areas.Admin.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

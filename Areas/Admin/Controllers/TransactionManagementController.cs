using Microsoft.AspNetCore.Mvc;

namespace Nhom12_EWallet.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TransactionManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

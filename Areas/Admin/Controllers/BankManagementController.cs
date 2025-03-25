using Microsoft.AspNetCore.Mvc;

namespace Nhom12_EWallet.Area.Admin.Controllers
{
    [Area("Admin")]
    public class BankManagementController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}

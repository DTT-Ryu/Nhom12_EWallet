using Microsoft.AspNetCore.Mvc;

namespace Nhom12_EWallet.Area.Admin.Controllers
{
    public class BankAccountManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

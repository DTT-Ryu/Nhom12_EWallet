﻿using Microsoft.AspNetCore.Mvc;

namespace Nhom12_EWallet.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

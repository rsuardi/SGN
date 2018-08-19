using System;
using Microsoft.AspNetCore.Mvc;

namespace SGN.Client.Controllers
{
	public class AuthorizationController : Controller
    {


        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace MDisasaterDampener.Controllers
{
    public class UserController:Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}

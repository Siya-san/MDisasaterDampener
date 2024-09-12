using MDisasaterDampener.Models;
using MDisasaterDampener.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDisasaterDampener.Controllers
{
    public class UserController:Controller 
    {
        UserServices userServices = new UserServices();
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
      
       
        public IActionResult ProcessLogin(LoginViewModel returningUser)
        {
            UserViewModel user = new UserViewModel();
            if (userServices.Login(returningUser) != null)
            {
                user = userServices.Login(returningUser);
                return RedirectToAction("Index", "Home");
            }
            else { return View("Login"); }

        }
        public IActionResult ProcessRegistation(RegisterViewModel user)
        {
            userServices.Register(user);
            return View("Login");

        }
    }
}

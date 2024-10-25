using MDisasaterDampener.Models;
using MDisasaterDampener.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MDisasaterDampener.Controllers
{
    public class UserController(IUserServices services) : Controller
    {
        private readonly IUserServices userServices = services;

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
            UserViewModel user = new();
            if (userServices.Login(returningUser) != null)
            {
                try
                {
                    DefaultHttpContext httpContext = new();
                    httpContext.Session.SetString("Current_User", JsonConvert.SerializeObject(userServices.Login(returningUser)));

                }
                catch
                {

                }



                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Login", user);
            }


        }

        public IActionResult ProcessRegistration(RegisterViewModel user)
        {
            userServices.Register(user);
            return View("Login");

        }
        public IActionResult AccountManagement()
        {

            return View();

        }
        [HttpPost]
        public IActionResult ProcessChangeUsername(UserViewModel user, int id)
        {
            if (ModelState.IsValid)
            {
                userServices.ChangeUsername(user, id);
                return RedirectToAction("Index", "Home");
            }
            return View("ChangePassword", user);
        }
        public IActionResult ProcessChangeEmail(UserViewModel user, int id)
        {
            if (ModelState.IsValid)
            {
                userServices.ChangeEmail(user, id);
                return RedirectToAction("Index", "Home");
            }
            return View("ChangePassword", user);
        }
        public IActionResult ProcessChangePassword(UserViewModel user, int id)
        {
            if (ModelState.IsValid)
            {
                userServices.ChangePassword(user, id);
                return RedirectToAction("Index", "Home");
            }
            return View("ChangePassword", user);
        }

    }
}

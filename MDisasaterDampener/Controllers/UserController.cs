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
                HttpContext.Session.SetString("ID", returningUser.id.ToString());
                HttpContext.Session.SetString("Username",returningUser.username );
     
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View("Login", user);
            }
         

        }
        public IActionResult ProcessRegistation(RegisterViewModel user)
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
        } public IActionResult ProcessChangeEmail(UserViewModel user, int id)
        {
            if (ModelState.IsValid)
            {
                userServices.ChangeEmail(user, id);
                return RedirectToAction("Index", "Home");
            }
            return View("ChangePassword", user);
        } public IActionResult ProcessChangePassword(UserViewModel user, int id)
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

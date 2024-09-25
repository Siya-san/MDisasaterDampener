using MDisasaterDampener.Models;
using MDisasaterDampener.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDisasaterDampener.Controllers
{
    public class ReliefController : Controller
    {
       ReliefServices reliefServices = new ReliefServices();
        public IActionResult ReliefEffort()
        {
            var viewModel = new ReliefViewModel
            {
                reliefEfforts = reliefServices.Read()
              
            };

            return View(viewModel);
         
        }  
        public IActionResult CreateNewReliefEffort()
        {
            return View();
        }
        public IActionResult ProcessCreateNewReliefEffort(ReliefEffortViewModel reliefEffort)
        {
            if (reliefEffort != null)
              reliefServices.Insert(reliefEffort);
            return RedirectToAction("Index", "Home");
        }
    }
}

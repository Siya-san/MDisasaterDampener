using MDisasaterDampener.Models;
using MDisasaterDampener.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDisasaterDampener.Controllers
{
   
    public class VolunteerController : Controller
    {
        VolunteerServices volunteerServices = new VolunteerServices();
        ReliefServices reliefServices = new ReliefServices();
        public IActionResult VolunteerCenter()
        {
            return View();
        }public IActionResult CreateVolunteerRequest()
        {
            var requestModel = new RequestViewModel()
            {
                volunteerRequest = new VolunteerRequestViewModel(),
                reliefEfforts = reliefServices.Read()

            };

            return View(requestModel);
        }
        public IActionResult ProcessVolunteerRequest()
        {
            return View();
        }
    }
}

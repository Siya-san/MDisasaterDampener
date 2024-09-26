using MDisasaterDampener.Models;
using MDisasaterDampener.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDisasaterDampener.Controllers
{
   
    public class VolunteerController : Controller
    {
        VolunteerServices volunteerServices = new VolunteerServices();
        ReliefServices reliefServices = new ReliefServices();
        public IActionResult VolunteerCenter()
        {
            var volunteerRequest = new RequestViewModel
            {
                volunteerRequests = volunteerServices.ReadRequest()

            };

            return View(volunteerRequest);
        }
        public IActionResult CreateVolunteerRequest()
        {
            var requestModel = new RequestViewModel()
            {
                volunteerRequest = new VolunteerRequestViewModel(),
                reliefEfforts = reliefServices.Read()

            };

            return View(requestModel);
        }
        public IActionResult ProcessVolunteerRequest(VolunteerRequestViewModel volunteerRequest)
        {
            if (volunteerRequest != null)
                volunteerServices.CreateRequest(volunteerRequest);
            else
                return View("CreateVolunteerRequest");
            return RedirectToAction("VolunteerCenter", "Volunteer");
        }
        public IActionResult ProcessVolunteer(int id)
        {
            UserViewModel user = new UserViewModel();
            if (HttpContext.Session.TryGetValue("Current_User", out var userDataBytes))
            {
                string userDataJson = Encoding.UTF8.GetString(userDataBytes);
                user = JsonConvert.DeserializeObject<UserViewModel>(userDataJson);
            }
            if (user != null && id > 0)
            {
                volunteerServices.CreateVolunteer(user.id, id);
                volunteerServices.UpdateNumberVolunteers(id);
            }

                return View("VolunteerCenter");
        }

    }
}

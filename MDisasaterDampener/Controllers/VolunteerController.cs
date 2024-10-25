using MDisasaterDampener.Models;
using MDisasaterDampener.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace MDisasaterDampener.Controllers
{

    public class VolunteerController : Controller
    {
        private readonly VolunteerServices volunteerServices = new();
        private readonly ReliefServices reliefServices = new();
        public IActionResult VolunteerCenter()
        {
            RequestViewModel volunteerRequest = new()
            {
                volunteerRequests = volunteerServices.ReadRequest()

            };

            return View(volunteerRequest);
        }
        public IActionResult CreateVolunteerRequest()
        {
            RequestViewModel requestModel = new()
            {
                volunteerRequest = new VolunteerRequestViewModel(),
                reliefEfforts = reliefServices.Read()

            };

            return View(requestModel);
        }
        public IActionResult ProcessVolunteerRequest(VolunteerRequestViewModel volunteerRequest)
        {
            if (volunteerRequest != null)
            {
                volunteerServices.CreateRequest(volunteerRequest);
            }
            else
            {
                return View("CreateVolunteerRequest");
            }

            return RedirectToAction("VolunteerCenter", "Volunteer");
        }
        public IActionResult ProcessVolunteer(int id)
        {
            UserViewModel user = new();
            if (HttpContext.Session.TryGetValue("Current_User", out byte[]? userDataBytes))
            {
                string userDataJson = Encoding.UTF8.GetString(userDataBytes);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                user = JsonConvert.DeserializeObject<UserViewModel>(userDataJson);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
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

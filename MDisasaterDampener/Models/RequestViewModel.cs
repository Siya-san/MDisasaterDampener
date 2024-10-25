namespace MDisasaterDampener.Models
{
#pragma warning disable IDE1006 // Naming Styles

    public class RequestViewModel
    {
        public VolunteerRequestViewModel volunteerRequest { get; set; }
        public List<ReliefEffortViewModel> reliefEfforts { get; set; }
        public List<VolunteerRequestViewModel> volunteerRequests { get; set; }
        public RequestViewModel()
        {
            volunteerRequest = new();
            reliefEfforts = [];
            volunteerRequests = [];
        }

    }
#pragma warning disable IDE1006 // Naming Styles

}

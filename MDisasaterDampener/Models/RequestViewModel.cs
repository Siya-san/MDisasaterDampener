namespace MDisasaterDampener.Models
{
    public class RequestViewModel
    {
        public VolunteerRequestViewModel volunteerRequest { get; set; }
        public List<ReliefEffortViewModel> reliefEfforts { get; set; }
        public List<VolunteerRequestViewModel> volunteerRequests{ get; set; }
        public RequestViewModel() {
            volunteerRequest = new VolunteerRequestViewModel();
            reliefEfforts = new List<ReliefEffortViewModel>();
            volunteerRequests = new List<VolunteerRequestViewModel>();
        }

    }
}

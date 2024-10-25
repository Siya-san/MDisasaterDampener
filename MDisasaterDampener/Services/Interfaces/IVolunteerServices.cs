using MDisasaterDampener.Models;

namespace MDisasaterDampener.Services.Interfaces
{
    public interface IVolunteerServices
    {
        public void UpdateNumberVolunteers(int Id);
        public void CreateRequest(VolunteerRequestViewModel volunteerRequest);
        public List<VolunteerRequestViewModel> ReadRequest();
        public VolunteerRequestViewModel ViewRequest(int id);
        public void CreateVolunteer(int Uid, int Vrid);

    }
}

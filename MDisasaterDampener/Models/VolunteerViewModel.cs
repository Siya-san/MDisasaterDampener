namespace MDisasaterDampener.Models
{
   
    public class VolunteerViewModel
    {
        public int Id { get; set; }
        public UserViewModel Uid { get; set; }
        public VolunteerRequestViewModel Vrid { get; set; }
        public VolunteerViewModel()
        {
            Uid = new UserViewModel();
            Vrid = new VolunteerRequestViewModel();
        }


    }
}

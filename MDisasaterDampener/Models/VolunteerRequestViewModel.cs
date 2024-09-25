namespace MDisasaterDampener.Models
{
    public class VolunteerRequestViewModel
    {
       
        public int Id { get; set; }
        public int NumVolunteers { get; set; }
        public DateOnly Date { get; set; }
        public string? Description { get; set; }
        public ReliefEffortViewModel Rid { get; set; }

        public VolunteerRequestViewModel()
        {
            Description = "Job description icluding the time and location where the volunteers are required";
            Rid=new ReliefEffortViewModel();
        }


    }
}

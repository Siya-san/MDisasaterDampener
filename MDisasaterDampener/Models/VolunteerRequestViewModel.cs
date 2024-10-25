namespace MDisasaterDampener.Models
{

#pragma warning disable CA1707 // Identifiers should not contain underscores
    public class VolunteerRequestViewModel
    {

        public int Id { get; set; }
        public int Number_Volunteers { get; set; }
        public DateOnly Date { get; set; }
        public string? Description { get; set; }
        public ReliefEffortViewModel Rid { get; set; }

        public VolunteerRequestViewModel()
        {
            Number_Volunteers = 1;
            Date = DateOnly.FromDateTime(DateTime.Now);
            Description = "Please indclude the job description, the time and location";
            Rid = new();
        }


    }
#pragma warning disable CA1707 // Identifiers should not contain underscores


}

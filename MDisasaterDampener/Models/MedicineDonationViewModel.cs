namespace MDisasaterDampener.Models
{
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA1707 // Identifiers should not contain underscores
    public class MedicineDonationViewModel
    {
        public int MD_Id { get; set; }
        public DateOnly Donation_Date { get; set; }
        public string? Description { get; set; }
        public DateOnly Expiry { get; set; }
        public int Unit_Type { get; set; }
        public ReliefEffortViewModel RE_Id { get; set; }
        public List<ReliefEffortViewModel> reliefEfforts { get; set; }
        public List<MedicineDonationViewModel> donations { get; set; }
        public MedicineDonationViewModel()
        {
            RE_Id = new();
            Expiry = DateOnly.FromDateTime(DateTime.Now);
            reliefEfforts = [];
            donations = [];
        }
    }
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE1006 // Naming Styles

}

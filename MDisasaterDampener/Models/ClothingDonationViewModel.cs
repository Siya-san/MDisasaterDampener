namespace MDisasaterDampener.Models
{
    public class ClothingDonationViewModel
    {
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA1707 // Identifiers should not contain underscores
        public int CD_Id { get; set; }

        public string? Item_Description { get; set; }
        public int Quantity { get; set; }
        public string? Material { get; set; }
        public string? Donation_Date { get; set; }
        public ReliefEffortViewModel RE_Id { get; set; }

        public List<ReliefEffortViewModel> reliefEfforts { get; set; }

        public List<ClothingDonationViewModel> donations { get; set; }
        public ClothingDonationViewModel()
        {
            RE_Id = new ReliefEffortViewModel();

            reliefEfforts = [];
            donations = [];
        }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE1006 // Naming Styles 
    }
}

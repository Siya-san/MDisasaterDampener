namespace MDisasaterDampener.Models
{
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CA1707 // Identifiers should not contain underscores
    public class FoodDonationViewModel
    {
        public int FD_Id { get; set; }
        public Categories Category { get; set; }

        public string? Item_Name { get; set; }
        public string? Description_and_inner_units { get; set; }
        public DateOnly Expiry { get; set; }
        public string? Weight { get; set; }
        public DateOnly Donation_Date { get; set; }
        public ReliefEffortViewModel RE_Id { get; set; }
        public List<ReliefEffortViewModel> reliefEfforts { get; set; }
        public List<FoodDonationViewModel> foodDonations { get; set; }
        public enum Categories
        {
            Fruit,
            Vegetables,
            Grains,

            Dairy,
            Protein,
            Fat

        }
        public FoodDonationViewModel()
        {
            RE_Id = new ReliefEffortViewModel();
            Expiry = DateOnly.FromDateTime(DateTime.Now);
            reliefEfforts = [];
            foodDonations = [];
        }

    }
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE1006 // Naming Styles

}

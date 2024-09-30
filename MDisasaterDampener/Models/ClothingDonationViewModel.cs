namespace MDisasaterDampener.Models
{
    public class ClothingDonationViewModel
    {
        public int CD_Id { get; set; }
        public string? Item_Description { get; set; }
        public int Quantity { get; set; }
        public string? Material { get; set; }
        public DateOnly Donation_Date { get; set; }
        public ReliefEffortViewModel RE_Id { get; set; }
        public List<ReliefEffortViewModel> reliefEfforts { get; set; }
        public List<ClothingDonationViewModel> donations { get; set; }
        public ClothingDonationViewModel()
        {
            RE_Id = new ReliefEffortViewModel();
          
            reliefEfforts = new List<ReliefEffortViewModel>();
            donations = new List<ClothingDonationViewModel>();
        }
    }
}

using MDisasaterDampener.Models;

namespace MDisasaterDampener.Services.Interfaces
{
    public interface IDonationServices
    {
        public void CreateFoodDonation(FoodDonationViewModel foodDonation);
        List<ClothingDonationViewModel> GetAllClothingDonations();
        public void CreateClothingDonation(ClothingDonationViewModel donation);
        public List<MedicineDonationViewModel> GetAllMedicineDonations();
        public void CreateMedicineDonation(MedicineDonationViewModel donation);
        public List<FoodDonationViewModel> GetAllFoodDonations();

    }
}

using MDisasaterDampener.Models;
using MDisasaterDampener.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDisasaterDampener.Controllers
{
    public class DonationController : Controller
    {
        private readonly DonationServices donationService = new();
        private readonly ReliefServices reliefService = new();
        public IActionResult DonateFood()
        {
            FoodDonationViewModel reliefEffots = new()
            {
                reliefEfforts = reliefService.Read()
            };
            return View(reliefEffots);
        }
        public IActionResult DonateMedicine()
        {
            MedicineDonationViewModel reliefEffots = new()
            {
                reliefEfforts = reliefService.Read()
            };
            return View(reliefEffots);
        }
        public IActionResult DonateClothes()
        {
            ClothingDonationViewModel reliefEffots = new()
            {
                reliefEfforts = reliefService.Read()
            };
            return View(reliefEffots);
        }
        public IActionResult CreateFoodDonation(FoodDonationViewModel foodDonation)
        {
            donationService.CreateFoodDonation(foodDonation);
            _ = RedirectToAction("Index", "Home");
            return Ok("Food Donation successfully created.");
        }


        public IActionResult GetAllFoodDonations()
        {
            FoodDonationViewModel allFoodDonations = new()
            {
                foodDonations = donationService.GetAllFoodDonations()
            };

            return View(allFoodDonations);
        }



        public IActionResult GetAllMedicineDonations()
        {
            MedicineDonationViewModel allMedicineDonations = new()
            {
                donations = donationService.GetAllMedicineDonations()
            };

            return View(allMedicineDonations);
        }
        public IActionResult CreateMedicineDonation(MedicineDonationViewModel medicineDonation)
        {
            donationService.CreateMedicineDonation(medicineDonation);
            _ = RedirectToAction("Index", "Home");
            return Ok("Medicine Donation successfully created.");
        }
        public IActionResult CreateClothingDonation(ClothingDonationViewModel clothingDonation)
        {
            donationService.CreateClothingDonation(clothingDonation);
            _ = RedirectToAction("Index", "Home");
            return Ok("Clothing Donation successfully created.");
        }

        public IActionResult GetAllClothingDonations()
        {
            ClothingDonationViewModel allClothingDonations = new()
            {
                donations = donationService.GetAllClothingDonations()
            };

            return View(allClothingDonations);
        }


    }
}

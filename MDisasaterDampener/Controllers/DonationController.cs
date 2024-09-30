using MDisasaterDampener.Models;
using MDisasaterDampener.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDisasaterDampener.Controllers
{
    public class DonationController : Controller
    {
        private readonly DonationServices donationService = new DonationServices();
        private readonly ReliefServices reliefService = new ReliefServices();
        public IActionResult DonateFood()
        {
            var reliefEffots = new FoodDonationViewModel
            {
                reliefEfforts = reliefService.Read()
            };
            return View(reliefEffots);
        } public IActionResult DonateMedicine()
        {
            var reliefEffots = new MedicineDonationViewModel
            {
                reliefEfforts = reliefService.Read()
            };
            return View(reliefEffots);
        } public IActionResult DonateClothes()
        {
            var reliefEffots = new ClothingDonationViewModel
            {
                reliefEfforts = reliefService.Read()
            };
            return View(reliefEffots);
        }
        public IActionResult CreateFoodDonation(FoodDonationViewModel foodDonation)
        {
            donationService.CreateFoodDonation(foodDonation);
            RedirectToAction("Index", "Home");
            return Ok("Food Donation successfully created.");
        }

   
        public IActionResult GetAllFoodDonations()
        {
            var allFoodDonations = new FoodDonationViewModel
            {
                foodDonations = donationService.GetAllFoodDonations()
            };
           
            return View(allFoodDonations);
        }
     

   
        public IActionResult GetAllMedicineDonations()
        {
            var allMedicineDonations = new MedicineDonationViewModel
            {
             donations = donationService.GetAllMedicineDonations()
            };
           
            return View(allMedicineDonations);
        }
        public IActionResult CreateMedicineDonation(MedicineDonationViewModel medicineDonation)
        {
            donationService.CreateMedicineDonation(medicineDonation);
            RedirectToAction("Index", "Home");
            return Ok("Medicine Donation successfully created.");
        }
   public IActionResult CreateClothingDonation(ClothingDonationViewModel clothingDonation)
        {
            donationService.CreateClothingDonation(clothingDonation);
            RedirectToAction("Index", "Home");
            return Ok("Clothing Donation successfully created.");
        }
   
        public IActionResult GetAllClothingDonations()
        {
            var allClothingDonations = new ClothingDonationViewModel
            {
                donations = donationService.GetAllClothingDonations()
            };
           
            return View(allClothingDonations);
        }

        
    }
}

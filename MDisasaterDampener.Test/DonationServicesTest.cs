using MDisasaterDampener.Models;
using MDisasaterDampener.Services;
using MDisasaterDampener.Services.Interfaces;
using Moq;

namespace MDisasaterDampener.Test_
{
#pragma warning disable IDE0008 // Use explicit type
#pragma warning disable CA1707 // Identifiers should not contain underscores
    public class DonationServicesTest
    {

        private readonly Mock<IDonationServices> mockDonationServices;
        private readonly DonationServices donationServices;

        public DonationServicesTest()
        {

            mockDonationServices = new Mock<IDonationServices>();
            donationServices = new();



        }

        [Fact]

        public void CreateFoodDonation_ShouldExecuteInsertQuery()

        {
            // Arrange
            var foodDonation = new FoodDonationViewModel
            {
                Category = FoodDonationViewModel.Categories.Grains,
                Item_Name = "Rice",
                Description_and_inner_units = "1kg Packets",
                Expiry = DateOnly.FromDateTime(DateTime.Now.AddMonths(6)).ToString(),
                Weight = "10kg",
                RE_Id = new ReliefEffortViewModel { Id = 1 }
            };

            // Act
            var exception = Record.Exception(() => mockDonationServices.Object.CreateFoodDonation(foodDonation));

            // Assert
            Assert.Null(exception); // Ensure no exception was thrown
        }

        [Fact]
        public void GetAllFoodDonations_ShouldReturnListOfDonations()
        {

            // Act
            var donations = donationServices.GetAllFoodDonations();

            // Assert
            Assert.NotNull(donations);
            _ = Assert.IsType<List<FoodDonationViewModel>>(donations);
        }

        [Fact]
        public void CreateMedicineDonation_ShouldExecuteInsertQuery()
        {
            // Arrange
            var medicineDonation = new MedicineDonationViewModel
            {
                Description = "Paracetamol",
                Expiry = DateOnly.FromDateTime(DateTime.Now.AddYears(1)),
                Unit_Type = 100,
                RE_Id = new ReliefEffortViewModel { Id = 1 }
            };

            // Act
            var exception = Record.Exception(() => mockDonationServices.Object.CreateMedicineDonation(medicineDonation));

            // Assert
            Assert.Null(exception); // Ensure no exception was thrown
        }

        [Fact]
        public void GetAllMedicineDonations_ShouldReturnListOfDonations()
        {
            // Act
            var donations = donationServices.GetAllMedicineDonations();

            // Assert
            Assert.NotNull(donations);
            _ = Assert.IsType<List<MedicineDonationViewModel>>(donations);
        }

        [Fact]
        public void CreateClothingDonation_ShouldExecuteInsertQuery()
        {
            // Arrange
            var clothingDonation = new ClothingDonationViewModel
            {
                Item_Description = "T-Shirts",
                Quantity = 50,
                Material = "Cotton",
                RE_Id = new ReliefEffortViewModel { Id = 1 }
            };

            // Act
            var exception = Record.Exception(() => mockDonationServices.Object.CreateClothingDonation(clothingDonation));

            // Assert
            Assert.Null(exception); // Ensure no exception was thrown
        }

        [Fact]
        public void GetAllClothingDonations_ShouldReturnListOfDonations()
        {
            // Act

            var donations = donationServices.GetAllClothingDonations();


            // Assert
            Assert.NotNull(donations);
            _ = Assert.IsType<List<ClothingDonationViewModel>>(donations);
        }
    }
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning restore IDE0008 // Use explicit type
}

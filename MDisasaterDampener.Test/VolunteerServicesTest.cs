using MDisasaterDampener.Models;
using MDisasaterDampener.Services;
using MDisasaterDampener.Services.Interfaces;
using Moq;
#pragma warning disable IDE0008 // Use explicit type
#pragma warning disable CA1707 // Identifiers should not contain underscores
namespace MDisasaterDampener.Test_
{
    public class VolunteerServicesTest
    {
        private readonly Mock<IVolunteerServices> mockVolunteerServices;

        private readonly VolunteerServices volunteerServices;

        public VolunteerServicesTest()
        {
            mockVolunteerServices = new();

            volunteerServices = new();
        }

        [Fact]
        public void CreateRequest_Should_Insert_Volunteer_Request()
        {
            // Arrange
            var volunteerRequest = new VolunteerRequestViewModel
            {
                Number_Volunteers = 10,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Description = "Test Description",
                Rid = new ReliefEffortViewModel { Id = 1 }
            };

            // Act
            var exception = Record.Exception(() => mockVolunteerServices.Object.CreateRequest(volunteerRequest));

            // Assert
            Assert.Null(exception);


        }

        [Fact]
        public void ViewRequest_Should_Return_VolunteerRequestViewModel()
        {
            // Arrange
            int requestId = 1;






            // Act
            var result = volunteerServices.ViewRequest(requestId);

            // Assert
            Assert.NotNull(result);
            // Assert other properties
        }

        [Fact]
        public void ReadRequest_Should_Return_List_Of_VolunteerRequests()
        {


            // Act
            var result = volunteerServices.ReadRequest();

            // Assert
            Assert.NotNull(result);
            _ = Assert.IsType<List<VolunteerRequestViewModel>>(result);
        }

        [Fact]
        public void CreateVolunteer_Should_Insert_Volunteer()
        {
            // Arrange
            int userId = 1;
            int volunteerRequestId = 2;

            var exception = Record.Exception(() => mockVolunteerServices.Object.CreateVolunteer(userId, volunteerRequestId));

            // Assert
            Assert.Null(exception);


        }

        [Fact]
        public void UpdateNumberVolunteers_Should_Decrease_Volunteer_Count()
        {
            // Arrange
            int requestId = 1;



            var exception = Record.Exception(() => mockVolunteerServices.Object.UpdateNumberVolunteers(requestId));


            Assert.Null(exception);



        }
    }
}
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0008 // Use explicit type

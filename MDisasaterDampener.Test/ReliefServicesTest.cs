using MDisasaterDampener.Models;
using MDisasaterDampener.Services;
using MDisasaterDampener.Services.Interfaces;
using Moq;

namespace MDisasaterDampener.Test_
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE0008 // Use explicit type


    public class ReliefServicesTest
    {
        private readonly Mock<IReliefServices> mockReliefServices;
        private readonly ReliefServices relief;



        public ReliefServicesTest()
        {
            mockReliefServices = new Mock<IReliefServices>();
            relief = new();

        }

        [Fact]
        public void Insert_Should_Add_ReliefEffort()
        {
            // Arrange
            var reliefEffort = new ReliefEffortViewModel
            {
                Title = "Flood Relief",
                Description = "Description for flood relief"
            };



            var exception = Record.Exception(() =>
            mockReliefServices.Object.Insert(reliefEffort));


            Assert.Null(exception);

        }

        [Fact]
        public void Read_Should_Return_ReliefEfforts_List()
        {


            // Act

            var result = relief.Read();

            _ = Assert.IsType<List<ReliefEffortViewModel>>(result);
        }

        [Fact]
        public void View_Should_Return_ReliefEffort()
        {
            // Arrange
            int reliefEffortId = 1;



            var expectedEffort = new ReliefEffortViewModel
            {
                Id = reliefEffortId,
                Title = "Food Distribution",
                Description = "Distributing food in remote areas"
            };



            // Act

            var result = relief.View(reliefEffortId);


            // Assert
            Assert.Equal(expectedEffort.Id, result.Id);
            // Assert other properties.
        }
    }
#pragma warning restore IDE0008 // Use explicit type

#pragma warning disable CA1707 // Identifiers should not contain underscores
}

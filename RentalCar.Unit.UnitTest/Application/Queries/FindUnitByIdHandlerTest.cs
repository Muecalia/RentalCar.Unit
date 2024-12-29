using FluentAssertions;
using Moq;
using RentalCar.Unit.Application.Handlers;
using RentalCar.Unit.Application.Queries.Request;
using RentalCar.Unit.Core.Entities;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;

namespace RentalCar.Unit.UnitTest.Application.Queries
{
    public class FindUnitByIdHandlerTest
    {
        [Fact]
        public async Task FindUnitById_Executed_Return_FindUnitResponse()
        {
            // Arrange
            var category = new Units
            {
                Id = "Id",
                Phone = "1234567890",
                Name = "Unidade",
                Address = "Endereço"
            };

            var categoryRepositoryMock = new Mock<IUnitRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            categoryRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(category);

            var findUnitByIdHandler = new FindUnitByIdHandler(categoryRepositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await findUnitByIdHandler.Handle(new FindUnitByIdRequest("Id"), It.IsAny<CancellationToken>());

            // Assert
            result.Data.Should().NotBeNull();
            result.Message.Should().NotBeNullOrEmpty();
            result.Succeeded.Should().BeTrue();

            categoryRepositoryMock.Verify(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

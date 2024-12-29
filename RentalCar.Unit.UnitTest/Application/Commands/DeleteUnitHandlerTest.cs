using FluentAssertions;
using Moq;
using RentalCar.Unit.Application.Commands.Request;
using RentalCar.Unit.Application.Handlers;
using RentalCar.Unit.Core.Entities;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;

namespace RentalCar.Unit.UnitTest.Application.Commands
{
    public class DeleteUnitHandlerTest
    {
        [Fact]
        public async Task DeleteUnit_Executed_Return_InputUnitResponse()
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
            categoryRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Units>(), It.IsAny<CancellationToken>()));

            var deleteUnitHandler = new DeleteUnitHandler(categoryRepositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await deleteUnitHandler.Handle(new DeleteUnitRequest("Id"), It.IsAny<CancellationToken>());

            // Assert
            result.Data.Should().NotBeNull();
            result.Message.Should().NotBeNullOrEmpty();
            result.Succeeded.Should().BeTrue();

            categoryRepositoryMock.Verify(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            categoryRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Units>(), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}

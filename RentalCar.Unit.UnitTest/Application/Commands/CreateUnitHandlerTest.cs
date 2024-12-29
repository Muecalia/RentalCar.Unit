using FluentAssertions;
using Moq;
using RentalCar.Unit.Application.Commands.Request;
using RentalCar.Unit.Application.Handlers;
using RentalCar.Unit.Core.Entities;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;

namespace RentalCar.Unit.UnitTest.Application.Commands
{
    public class CreateUnitHandlerTest
    {
        [Fact]
        public async Task CreateUnit_Executed_return_InputUnitResponse()
        {
            // Arrange
            var category = new Units
            {
                Id = "Id",
                Name = "Unidade 1",
                Address = "Endereço"
            };

            var createUnitRequest = new CreateUnitRequest 
            {
                Name = "Unidade",
                Address = "Endereço"
            };

            var categoryRepositoryMock = new Mock<IUnitRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            categoryRepositoryMock.Setup(repo => repo.IsUnitExist(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
            categoryRepositoryMock.Setup(repo => repo.Create(It.IsAny<Units>(), It.IsAny<CancellationToken>())).ReturnsAsync(category);

            var createUnitHandler = new CreateUnitHandler(categoryRepositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await createUnitHandler.Handle(createUnitRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Data.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Message.Should().NotBeNullOrEmpty();

            categoryRepositoryMock.Verify(repo => repo.IsUnitExist(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            categoryRepositoryMock.Verify(repo => repo.Create(It.IsAny<Units>(), It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}

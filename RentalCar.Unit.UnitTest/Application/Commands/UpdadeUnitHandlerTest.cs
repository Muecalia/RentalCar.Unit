using FluentAssertions;
using Moq;
using RentalCar.Unit.Application.Commands.Request;
using RentalCar.Unit.Application.Handlers;
using RentalCar.Unit.Core.Entities;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;

namespace RentalCar.Unit.UnitTest.Application.Commands
{
    public class UpdadeUnitHandlerTest
    {
        [Fact]
        public async Task UpdadeUnit_Executed_Return_InputUnitResponse()
        {
            // Arrange
            var category = new Units
            {
                Id = "Id",
                Phone = "1234567890",
                Name = "Unidade",
                Address = "Endereço"
            };

            var updateUnitRequest = new UpdateUnitRequest 
            {
                Id = "Id",
                Phone = "1234567890",
                Name = "Unidade Atualizada",
                Address = "Endereço Atualizado"
            };

            var categoryRepositoryMock = new Mock<IUnitRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            categoryRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(category);
            categoryRepositoryMock.Setup(repo => repo.Update(It.IsAny<Units>(), It.IsAny<CancellationToken>()));

            var updadeUnitHandler = new UpdadeUnitHandler(categoryRepositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await updadeUnitHandler.Handle(updateUnitRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Data.Should().NotBeNull();
            result.Message.Should().NotBeNullOrEmpty();
            result.Succeeded.Should().BeTrue();

            categoryRepositoryMock.Verify(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
            categoryRepositoryMock.Verify(repo => repo.Update(It.IsAny<Units>(), It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}

using FluentAssertions;
using Moq;
using RentalCar.Unit.Application.Handlers;
using RentalCar.Unit.Application.Queries.Request;
using RentalCar.Unit.Core.Entities;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;

namespace RentalCar.Unit.UnitTest.Application.Queries
{
    public class FindAllUnitHandlerTest
    {
        [Fact]
        public async Task FindAllUnits_Executed_Return_List_FindUnitResponse()
        {
            // Arrange
            var categories = new List<Units> 
            {
                new() { Id = "Id 1", Name = "Unidade 1", Phone = "1234567890", Address = "Endereço 1" },
                new() { Id = "Id 2", Name = "Unidade 2", Phone = "1234567890", Address = "Endereço 2" },
                new() { Id = "Id 3", Name = "Unidade 3", Phone = "1234567890", Address = "Endereço 3" },
                new() { Id = "Id 4", Name = "Unidade 4", Phone = "1234567890", Address = "Endereço 4" },
            };

            var categoryRepositoryMock = new Mock<IUnitRepository>();
            var loggerServiceMock = new Mock<ILoggerService>();
            var prometheusServiceMock = new Mock<IPrometheusService>();

            categoryRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(categories);

            var findAllUnitsHandler = new FindAllUnitsHandler(categoryRepositoryMock.Object, loggerServiceMock.Object, prometheusServiceMock.Object);

            // Act
            var result = await findAllUnitsHandler.Handle(new FindAllUnitsRequest(1, 5), It.IsAny<CancellationToken>());

            // Assert
            result.Datas.Should().NotBeNull();
            result.Message.Should().NotBeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
            result.Datas.Count.Should().Be(categories.Count);

            categoryRepositoryMock.Verify(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

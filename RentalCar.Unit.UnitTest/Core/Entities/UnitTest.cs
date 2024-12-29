using FluentAssertions;
using RentalCar.Unit.Core.Entities;

namespace RentalCar.Unit.UnitTest.Core.Entities
{
    public class UnitTest
    {
        [Fact]
        public void Unit_Success()
        {
            // Arrange
            var unit = new Units
            {
                Id = "Id",
                Name = "Unidade",
                Address = "Endereço"
            };

            // Act

            //Assert
            unit.Should().NotBeNull();
            unit.Name.Should().NotBeNullOrEmpty();
            unit.CreatedAt.Date.Should().Be(DateTime.Now.Date);
        }
    }
}

using MediatR;
using RentalCar.Unit.Core.Wrappers;
using RentalCar.Unit.Application.Commands.Response;

namespace RentalCar.Unit.Application.Commands.Request
{
    public class CreateUnitRequest : IRequest<ApiResponse<InputUnitResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}

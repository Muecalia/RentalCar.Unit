using MediatR;
using RentalCar.Unit.Core.Wrappers;
using RentalCar.Unit.Application.Commands.Response;

namespace RentalCar.Unit.Application.Commands.Request
{
    public class UpdateUnitRequest : IRequest<ApiResponse<InputUnitResponse>>
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}

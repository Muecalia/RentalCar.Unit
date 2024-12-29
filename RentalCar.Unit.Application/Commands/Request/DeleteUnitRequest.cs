using MediatR;
using RentalCar.Unit.Core.Wrappers;
using RentalCar.Unit.Application.Commands.Response;

namespace RentalCar.Unit.Application.Commands.Request
{
    public class DeleteUnitRequest(string id) : IRequest<ApiResponse<InputUnitResponse>>
    {
        public string Id { get; set; } = id;
    }
}

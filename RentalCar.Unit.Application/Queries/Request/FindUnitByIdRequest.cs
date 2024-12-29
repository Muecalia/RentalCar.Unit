using MediatR;
using RentalCar.Unit.Core.Wrappers;
using RentalCar.Unit.Application.Queries.Response;

namespace RentalCar.Unit.Application.Queries.Request
{
    public class FindUnitByIdRequest(string id) : IRequest<ApiResponse<FindUnitResponse>>
    {
        public string Id { get; set; } = id;
    }
}

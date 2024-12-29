using MediatR;
using RentalCar.Unit.Application.Queries.Response;
using RentalCar.Unit.Core.Wrappers;

namespace RentalCar.Unit.Application.Queries.Request
{
    public class FindAllUnitsRequest(int pageNumber, int pageSize)  : IRequest<PagedResponse<FindUnitResponse>>
    {
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
    }
}

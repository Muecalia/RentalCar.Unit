using MediatR;
using Microsoft.AspNetCore.Authorization;
using RentalCar.Unit.Application.Commands.Request;
using RentalCar.Unit.Application.Queries.Request;

namespace RentalCar.Unit.API.Endpoints
{
    public static class UnitEndPoint
    {
        public static void MapUnitEndPoints(this IEndpointRouteBuilder route) 
        {
            // Get All
            route.MapGet("/unit", [Authorize(Roles = "Admin")] async (IMediator mediator, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10) => 
            {
                var result = await mediator.Send(new FindAllUnitsRequest(pageNumber, pageSize), cancellationToken);
                return Results.Ok(result);
            });

            // Get By Id
            route.MapGet("/unit/{id}", [Authorize(Roles = "Admin")] async (string id, IMediator mediator, CancellationToken cancellationToken) => 
            {
                var result = await mediator.Send(new FindUnitByIdRequest(id), cancellationToken);
                return result.Succeeded ? Results.Ok(result) : Results.NotFound(result.Message);
            });

            // Create Unit
            route.MapPost("/unit", [Authorize(Roles = "Admin")] async (CreateUnitRequest request, IMediator mediator, CancellationToken cancellationToken) => 
            {
                var result = await mediator.Send(request, cancellationToken);
                return result.Succeeded ? Results.Created("", result) : Results.BadRequest(result.Message);
            });

            // Delete Unit
            route.MapDelete("/unit/{id}", [Authorize(Roles = "Admin")] async (string id, IMediator mediator, CancellationToken cancellationToken) => 
            {
                var result = await mediator.Send(new DeleteUnitRequest(id), cancellationToken);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Message);
            });

            // Update Unit
            route.MapPut("/unit/{id}", async (string id, UpdateUnitRequest request, IMediator mediator, CancellationToken cancellationToken) => 
            {
                request.Id = id;
                var result = await mediator.Send(request, cancellationToken);
                return result.Succeeded ? Results.Ok(result) : Results.BadRequest(result.Message);
            });
        }
    }
}

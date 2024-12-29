using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;
using RentalCar.Unit.Application.Queries.Request;
using RentalCar.Unit.Application.Queries.Response;
using RentalCar.Unit.Core.Configs;
using RentalCar.Unit.Core.Wrappers;

namespace RentalCar.Unit.Application.Handlers
{
    public class FindAllUnitsHandler : IRequestHandler<FindAllUnitsRequest, PagedResponse<FindUnitResponse>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ILoggerService _loggerService;
        private readonly IPrometheusService _prometheusService;

        public FindAllUnitsHandler(IUnitRepository unitRepository, ILoggerService loggerService, IPrometheusService prometheusService)
        {
            _unitRepository = unitRepository;
            _loggerService = loggerService;
            _prometheusService = prometheusService;
        }

        public async Task<PagedResponse<FindUnitResponse>> Handle(FindAllUnitsRequest request, CancellationToken cancellationToken)
        {
            const string Objecto = "unidades";
            try
            {
                var categories = await _unitRepository.GetAll(request.PageNumber, request.PageSize, cancellationToken).ConfigureAwait(false);;
                _prometheusService.AddFindAllUnitsCounter(StatusCodes.Status200OK.ToString());
                var results = categories.Select(unit => new FindUnitResponse(unit.Id, unit.Name, unit.Phone, unit.Address)).ToList();
                return new PagedResponse<FindUnitResponse>(results, request.PageNumber, request.PageSize, results.Count, MessageError.CarregamentoSucesso(Objecto, results.Count));
            }
            catch (Exception ex)
            {
                _prometheusService.AddFindAllUnitsCounter(StatusCodes.Status400BadRequest.ToString());
                _loggerService.LogError(MessageError.CarregamentoErro(Objecto, ex.Message));
                return new PagedResponse<FindUnitResponse>(MessageError.CarregamentoErro(Objecto));
                //throw;
            }
        }

    }
}

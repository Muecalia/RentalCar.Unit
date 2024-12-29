using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;
using RentalCar.Unit.Application.Commands.Request;
using RentalCar.Unit.Application.Commands.Response;
using RentalCar.Unit.Core.Configs;
using RentalCar.Unit.Core.Wrappers;

namespace RentalCar.Unit.Application.Handlers
{
    public class DeleteUnitHandler : IRequestHandler<DeleteUnitRequest, ApiResponse<InputUnitResponse>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ILoggerService _loggerService;
        private readonly IPrometheusService _prometheusService;

        public DeleteUnitHandler(IUnitRepository unitRepository, ILoggerService loggerService, IPrometheusService prometheusService)
        {
            _unitRepository = unitRepository;
            _loggerService = loggerService;
            _prometheusService = prometheusService;
        }

        public async Task<ApiResponse<InputUnitResponse>> Handle(DeleteUnitRequest request, CancellationToken cancellationToken)
        {
            const string Objecto = "unidade";
            const string Operacao = "eliminar";
            try
            {
                var unit = await _unitRepository.GetById(request.Id, cancellationToken);
                if (unit is null)
                {
                    _prometheusService.AddDeleteUnitCounter(StatusCodes.Status404NotFound.ToString());
                    _loggerService.LogWarning(MessageError.NotFound(Objecto, request.Id));
                    return ApiResponse<InputUnitResponse>.Error(MessageError.NotFound(Objecto));
                }

                await _unitRepository.Delete(unit, cancellationToken);

                var result = new InputUnitResponse(unit.Id, unit.Name, unit.Address);
                _prometheusService.AddDeleteUnitCounter(StatusCodes.Status200OK.ToString());
                return ApiResponse<InputUnitResponse>.Success(result, MessageError.OperacaoSucesso(Objecto, Operacao));
            }
            catch (Exception ex)
            {
                _prometheusService.AddDeleteUnitCounter(StatusCodes.Status400BadRequest.ToString());
                _loggerService.LogError(MessageError.OperacaoErro(Objecto, Operacao, ex.Message));
                return ApiResponse<InputUnitResponse>.Error(MessageError.OperacaoErro(Objecto, Operacao));
                //throw;
            }
        }
    }
}

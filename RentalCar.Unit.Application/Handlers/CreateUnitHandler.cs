using MediatR;
using Microsoft.AspNetCore.Http;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;
using RentalCar.Unit.Application.Commands.Request;
using RentalCar.Unit.Application.Commands.Response;
using RentalCar.Unit.Core.Configs;
using RentalCar.Unit.Core.Entities;
using RentalCar.Unit.Core.Wrappers;

namespace RentalCar.Unit.Application.Handlers
{
    public class CreateUnitHandler : IRequestHandler<CreateUnitRequest, ApiResponse<InputUnitResponse>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ILoggerService _loggerService;
        private readonly IPrometheusService _prometheusService;

        public CreateUnitHandler(IUnitRepository unitRepository, ILoggerService loggerService, IPrometheusService prometheusService)
        {
            _unitRepository = unitRepository;
            _loggerService = loggerService;
            _prometheusService = prometheusService;
        }

        public async Task<ApiResponse<InputUnitResponse>> Handle(CreateUnitRequest request, CancellationToken cancellationToken)
        {
            const string Objecto = "unidade";
            const string Operacao = "registar";
            try
            {
                if (await _unitRepository.IsUnitExist(request.Name, cancellationToken))
                {
                    _loggerService.LogWarning(MessageError.Conflito($"{Objecto} {request.Name}"));
                    _prometheusService.AddNewUnitCounter(StatusCodes.Status409Conflict.ToString());
                    return ApiResponse<InputUnitResponse>.Error(MessageError.Conflito(Objecto));
                }

                var newUnit = new Units
                {
                    Name = request.Name,
                    Phone = request.Phone,
                    Address = request.Address
                };

                var unit = await _unitRepository.Create(newUnit, cancellationToken);

                var result = new InputUnitResponse(unit.Id, unit.Name, unit.Phone);
                _prometheusService.AddNewUnitCounter(StatusCodes.Status201Created.ToString());
                return ApiResponse<InputUnitResponse>.Success(result, MessageError.OperacaoSucesso(Objecto, Operacao));
            }
            catch (Exception ex)
            {
                _prometheusService.AddNewUnitCounter(StatusCodes.Status400BadRequest.ToString());
                _loggerService.LogError(MessageError.OperacaoErro(Objecto, Operacao, ex.Message));
                return ApiResponse<InputUnitResponse>.Error(MessageError.OperacaoErro(Objecto, Operacao));
                //throw;
            }
        }

    }
}

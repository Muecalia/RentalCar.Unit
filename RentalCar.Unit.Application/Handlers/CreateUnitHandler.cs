using MediatR;
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

        public CreateUnitHandler(IUnitRepository unitRepository, ILoggerService loggerService)
        {
            _unitRepository = unitRepository;
            _loggerService = loggerService;
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

                return ApiResponse<InputUnitResponse>.Success(result, MessageError.OperacaoSucesso(Objecto, Operacao));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MessageError.OperacaoErro(Objecto, Operacao, ex.Message));
                return ApiResponse<InputUnitResponse>.Error(MessageError.OperacaoErro(Objecto, Operacao));
                //throw;
            }
        }

    }
}

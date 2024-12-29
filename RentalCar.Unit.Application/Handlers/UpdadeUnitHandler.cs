using MediatR;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;
using RentalCar.Unit.Application.Commands.Request;
using RentalCar.Unit.Application.Commands.Response;
using RentalCar.Unit.Core.Configs;
using RentalCar.Unit.Core.Wrappers;

namespace RentalCar.Unit.Application.Handlers
{
    public class UpdadeUnitHandler : IRequestHandler<UpdateUnitRequest, ApiResponse<InputUnitResponse>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ILoggerService _loggerService;

        public UpdadeUnitHandler(IUnitRepository unitRepository, ILoggerService loggerService)
        {
            _unitRepository = unitRepository;
            _loggerService = loggerService;
        }

        public async Task<ApiResponse<InputUnitResponse>> Handle(UpdateUnitRequest request, CancellationToken cancellationToken)
        {
            const string Objecto = "unidade";
            const string Operacao = "editar";
            try
            {
                var unit = await _unitRepository.GetById(request.Id, cancellationToken);
                if (unit == null)
                {
                    _loggerService.LogWarning(MessageError.NotFound(Objecto, request.Id));
                    return ApiResponse<InputUnitResponse>.Error(MessageError.NotFound(Objecto));
                }

                unit.Name = request.Name;
                unit.Phone = request.Phone;
                unit.Address = request.Address;

                await _unitRepository.Update(unit, cancellationToken);

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

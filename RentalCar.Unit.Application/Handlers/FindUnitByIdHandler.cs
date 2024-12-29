using MediatR;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;
using RentalCar.Unit.Application.Queries.Request;
using RentalCar.Unit.Application.Queries.Response;
using RentalCar.Unit.Core.Configs;
using RentalCar.Unit.Core.Wrappers;

namespace RentalCar.Unit.Application.Handlers
{
    public class FindUnitByIdHandler : IRequestHandler<FindUnitByIdRequest, ApiResponse<FindUnitResponse>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ILoggerService _loggerService;

        public FindUnitByIdHandler(IUnitRepository unitRepository, ILoggerService loggerService)
        {
            _unitRepository = unitRepository;
            _loggerService = loggerService;
        }

        public async Task<ApiResponse<FindUnitResponse>> Handle(FindUnitByIdRequest request, CancellationToken cancellationToken)
        {
            const string Objecto = "unidade";
            try
            {
                var unit = await _unitRepository.GetById(request.Id, cancellationToken);
                if (unit is null)
                {
                    _loggerService.LogWarning(MessageError.NotFound(Objecto, request.Id));
                    return ApiResponse<FindUnitResponse>.Error(MessageError.NotFound(Objecto));
                }

                var result = new FindUnitResponse(unit.Id, unit.Name, unit.Phone, unit.Address);

                _loggerService.LogInformation(MessageError.CarregamentoSucesso(Objecto, 1));
                return ApiResponse<FindUnitResponse>.Success(result, MessageError.CarregamentoSucesso(Objecto));
            }
            catch (Exception ex)
            {
                _loggerService.LogError(MessageError.CarregamentoErro(Objecto, ex.Message));
                return ApiResponse<FindUnitResponse>.Error(MessageError.CarregamentoErro(Objecto));
                //throw;
            }
        }

    }
}

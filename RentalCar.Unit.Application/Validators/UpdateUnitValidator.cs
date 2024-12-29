using FluentValidation;
using RentalCar.Unit.Application.Commands.Request;

namespace RentalCar.Unit.Application.Validators
{
    public class UpdateUnitValidator : AbstractValidator<UpdateUnitRequest>
    {
        public UpdateUnitValidator()
        {
            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("Informe o código");

            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Informe o nome");
            
            RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("Informe o telefone");

            RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Informa o endereço");

        }
    }
}

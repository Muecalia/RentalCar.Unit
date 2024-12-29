using FluentValidation;
using RentalCar.Unit.Application.Commands.Request;

namespace RentalCar.Unit.Application.Validators
{
    public class CreateUnitValidator : AbstractValidator<CreateUnitRequest>
    {
        public CreateUnitValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Informe o nome");
            
            RuleFor(u => u.Phone)
                .NotEmpty().WithMessage("Informe o telefone");

            RuleFor(u => u.Address)
                .NotEmpty().WithMessage("Informa o endereço");
        }
    }
}

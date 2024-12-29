using FluentValidation;
using RentalCar.Unit.Application.Commands.Request;

namespace RentalCar.Unit.Application.Validators
{
    public class DeleteUnitValidator : AbstractValidator<DeleteUnitRequest>
    {
        public DeleteUnitValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("Informe o código");
        }
    }
}

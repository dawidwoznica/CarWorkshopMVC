using CarWorkshop.Domain.Interfaces;
using FluentValidation;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshopCommand
{
    public class EditCarWorkshopCommandValidator : AbstractValidator<EditCarWorkshopCommand>
    {
        public EditCarWorkshopCommandValidator(ICarWorkshopRepository carWorkshopRepository)
        {
            RuleFor(w => w.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(w => w.PhoneNumber)
                .MinimumLength(8).WithMessage("Name must be between 8 and 12 characters.")
                .MaximumLength(12).WithMessage("Name must be between 8 and 12 characters.");
        }
    }
}
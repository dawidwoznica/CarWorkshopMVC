namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshopCommand;

using Domain.Interfaces;
using FluentValidation;

public class CreateCarWorkshopCommandValidator : AbstractValidator<CreateCarWorkshopCommand>
{
    public CreateCarWorkshopCommandValidator(ICarWorkshopRepository carWorkshopRepository)
    {
        RuleFor(w => w.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be between 2 and 20 characters.")
            .MaximumLength(20).WithMessage("Name must be between 2 and 20 characters.")
            .Custom((value, context) =>
            {
                var existingCarWorkshop = carWorkshopRepository.GetByName(value).Result;

                if (existingCarWorkshop != null)
                    context.AddFailure($"Car workshop with name: {value} already exists.");
            });

        RuleFor(w => w.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(w => w.PhoneNumber)
            .MinimumLength(8).WithMessage("Name must be between 8 and 12 characters.")
            .MaximumLength(12).WithMessage("Name must be between 8 and 12 characters.");
    }
}
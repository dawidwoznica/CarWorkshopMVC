namespace CarWorkshop.Application.CarWorkshopService.Commands;

using FluentValidation;

public class CreateCarWorkshopServiceCommandValidator : AbstractValidator<CreateCarWorkshopServiceCommand>
{
    public CreateCarWorkshopServiceCommandValidator()
    {
        RuleFor(s => s.Cost).NotEmpty().NotNull();
        RuleFor(s => s.Description).NotEmpty().NotNull();
        RuleFor(s => s.CarWorkshopEncodedName).NotEmpty().NotNull();
    }
}
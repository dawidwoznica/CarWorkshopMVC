namespace CarWorkshop.Application.Tests.CarWorkshopService.Commands.CreateCarWorkshopServiceCommand;

using Application.CarWorkshopService.Commands;
using FluentValidation.TestHelper;
using Xunit;

public class CreateCarWorkshopServiceCommandValidatorTest
{

    [Fact]
    public void Validate_WithValidCommand_ShouldNotHaveValidationError()
    {
        var validator = new CreateCarWorkshopServiceCommandValidator();
        var command = new CreateCarWorkshopServiceCommand()
        {
            Cost = "100 PLN",
            Description = "Description",
            CarWorkshopEncodedName = "workshop1"
        };

        var result = validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_WithInvalidCommand_ShouldHaveValidationError()
    {
        var validator = new CreateCarWorkshopServiceCommandValidator();
        var command = new CreateCarWorkshopServiceCommand()
        {
            Cost = "",
            Description = null,
            CarWorkshopEncodedName = ""
        };

        var result = validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Cost);
        result.ShouldHaveValidationErrorFor(c => c.Description);
        result.ShouldHaveValidationErrorFor(c => c.CarWorkshopEncodedName);
    }
}
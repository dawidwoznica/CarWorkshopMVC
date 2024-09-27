using CarWorkshop.Application.CarWorkshopService.Commands;

namespace CarWorkshop.Application.Tests.CarWorkshopService.Commands.CreateCarWorkshopServiceCommand;

using System.Threading;
using System.Threading.Tasks;
using Application.ApplicationUser;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Moq;
using Xunit;
using CreateCarWorkshopServiceCommand = global::CarWorkshop.Application.CarWorkshopService.Commands.CreateCarWorkshopServiceCommand;

public class CreateCarWorkshopServiceCommandHandlerTest
{

    [Fact]
    public async Task Handle_CreateCarWorkshopService_WhenUserIsAuthorized()
    {
        var carWorkshop = new CarWorkshop()
        {
            Id = 1,
            CreatedById = "1"
        };

        var command = new CreateCarWorkshopServiceCommand()
        {
            Cost = "100 PLN",
            Description = "Description",
            CarWorkshopEncodedName = "workshop1"
        };

        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrentUser())
            .Returns(new CurrentUser("1", "test@test.com", ["User"]));

        var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
        carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

        var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

        var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object, userContextMock.Object);

        await handler.Handle(command, CancellationToken.None);

        carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<CarWorkshopService>()), Times.Once);
    }

    [Fact]
    public async Task Handle_CreateCarWorkshopService_WhenUserIsModerator()
    {
        var carWorkshop = new CarWorkshop()
        {
            Id = 1,
            CreatedById = "1"
        };

        var command = new CreateCarWorkshopServiceCommand()
        {
            Cost = "100 PLN",
            Description = "Description",
            CarWorkshopEncodedName = "workshop1"
        };

        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrentUser())
            .Returns(new CurrentUser("2", "test@test.com", ["Moderator"]));

        var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
        carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

        var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

        var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object, userContextMock.Object);

        await handler.Handle(command, CancellationToken.None);

        carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<CarWorkshopService>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DoesntCreateCarWorkshopService_WhenUserIsNotAuthorised()
    {
        var carWorkshop = new CarWorkshop()
        {
            Id = 1,
            CreatedById = "1"
        };

        var command = new CreateCarWorkshopServiceCommand()
        {
            Cost = "100 PLN",
            Description = "Description",
            CarWorkshopEncodedName = "workshop1"
        };

        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrentUser())
            .Returns(new CurrentUser("2", "test@test.com", ["User"]));

        var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
        carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

        var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

        var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object, userContextMock.Object);

        await handler.Handle(command, CancellationToken.None);

        carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<CarWorkshopService>()), Times.Never);
    }

    [Fact]
    public async Task Handle_DoesntCreateCarWorkshopService_WhenUserIsNull()
    {
        var carWorkshop = new CarWorkshop()
        {
            Id = 1,
            CreatedById = "1"
        };

        var command = new CreateCarWorkshopServiceCommand()
        {
            Cost = "100 PLN",
            Description = "Description",
            CarWorkshopEncodedName = "workshop1"
        };

        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrentUser())
            .Returns((CurrentUser?)null);

        var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
        carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName)).ReturnsAsync(carWorkshop);

        var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();

        var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopRepositoryMock.Object, carWorkshopServiceRepositoryMock.Object, userContextMock.Object);

        await handler.Handle(command, CancellationToken.None);

        carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<CarWorkshopService>()), Times.Never);
    }
}
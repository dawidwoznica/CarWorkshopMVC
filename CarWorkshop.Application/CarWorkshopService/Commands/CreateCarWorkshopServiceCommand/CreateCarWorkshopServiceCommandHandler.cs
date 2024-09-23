namespace CarWorkshop.Application.CarWorkshopService.Commands;

using ApplicationUser;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using MediatR;

public class CreateCarWorkshopServiceCommandHandler(ICarWorkshopRepository carWorkshopRepository,
    ICarWorkshopServiceRepository carWorkshopServiceRepository, IUserContext userContext)
    : IRequestHandler<CreateCarWorkshopServiceCommand>
{
    public async Task Handle(CreateCarWorkshopServiceCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        var carWorkshop = await carWorkshopRepository.GetByEncodedName(request.CarWorkshopEncodedName!);
        var isEditable = currentUser != null && (carWorkshop.CreatedById == currentUser.Id || currentUser.IsInRole("Moderator"));

        if (!isEditable)
            return;

        var carWorkshopService = new CarWorkshopService
        {
            Cost = request.Cost,
            Description = request.Description,
            CarWorkshopId = carWorkshop.Id
        };

        await carWorkshopServiceRepository.Create(carWorkshopService);
    }
}
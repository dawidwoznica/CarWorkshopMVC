using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshopCommand
{
    public class CreateCarWorkshopCommandHandler(ICarWorkshopRepository carWorkshopRepository, IMapper mapper, IUserContext userContext)
        : IRequestHandler<CreateCarWorkshopCommand>
    {
        public async Task Handle(CreateCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();

            if (!currentUser.IsInRole("Moderator"))
                return;

            var carWorkshop = mapper.Map<Domain.Entities.CarWorkshop>(request);
            carWorkshop.EncodeName();

            carWorkshop.CreatedById = currentUser.Id;

            await carWorkshopRepository.Create(carWorkshop);
        }
    }
}

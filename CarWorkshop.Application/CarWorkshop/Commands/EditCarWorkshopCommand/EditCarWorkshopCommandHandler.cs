using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshopCommand
{
    public class EditCarWorkshopCommandHandler(ICarWorkshopRepository carWorkshopRepository, IUserContext userContext)
        : IRequestHandler<EditCarWorkshopCommand>
    {
        public async Task Handle(EditCarWorkshopCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            var carWorkshop = await carWorkshopRepository.GetByEncodedName(request.EncodedName!);
            var isEditable = carWorkshop.CreatedById == currentUser.Id || currentUser.IsInRole("Moderator");

            if (!isEditable) 
                return;

            carWorkshop.Description = request.Description;
            carWorkshop.About = request.About;
            carWorkshop.ContactDetails.Street = request.Street;
            carWorkshop.ContactDetails.City = request.City;
            carWorkshop.ContactDetails.PhoneNumber = request.PhoneNumber;
            carWorkshop.ContactDetails.PostalCode = request.PostalCode;

            await carWorkshopRepository.Commit();
        }
    }
}

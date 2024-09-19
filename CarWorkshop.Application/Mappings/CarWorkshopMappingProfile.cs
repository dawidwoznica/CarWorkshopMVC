using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshopCommand;
using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Application.Mappings
{
    using CarWorkshopService;
    using CarWorkshopService = global::CarWorkshop.Domain.Entities.CarWorkshopService;

    public class CarWorkshopMappingProfile : Profile
    {
        public CarWorkshopMappingProfile(IUserContext userContext)
        {
            var currentUser = userContext.GetCurrentUser();

            CreateMap<CarWorkshopDto, Domain.Entities.CarWorkshop>()
                .ForMember(e => e.ContactDetails, opt => opt.MapFrom(src => new CarWorkshopContactDetails()
                {
                    City = src.City,
                    Street = src.Street,
                    PostalCode = src.PostalCode,
                    PhoneNumber = src.PhoneNumber
                }));

            CreateMap<Domain.Entities.CarWorkshop, CarWorkshopDto>()
                .ForMember(dto => dto.IsEditable, opt => opt.MapFrom(src => src.CreatedById == currentUser.Id || currentUser.IsInRole("Moderator")))
                .ForMember(dto => dto.Street, opt => opt.MapFrom(src => src.ContactDetails.Street))
                .ForMember(dto => dto.City, opt => opt.MapFrom(src => src.ContactDetails.City))
                .ForMember(dto => dto.PhoneNumber, opt => opt.MapFrom(src => src.ContactDetails.PhoneNumber))
                .ForMember(dto => dto.PostalCode, opt => opt.MapFrom(src => src.ContactDetails.PostalCode));

            CreateMap<CarWorkshopDto, EditCarWorkshopCommand>();

            CreateMap<CarWorkshopServiceDto, CarWorkshopService>()
                .ReverseMap();
        }
    }
}

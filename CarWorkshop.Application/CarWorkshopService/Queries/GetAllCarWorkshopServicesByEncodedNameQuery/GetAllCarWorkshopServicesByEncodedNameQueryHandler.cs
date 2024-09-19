namespace CarWorkshop.Application.CarWorkshopService.Queries.GetAllCarWorkshopsByEncodedNameQuery;

using ApplicationUser;
using AutoMapper;
using Infrastructure.Repositories;
using MediatR;

public class GetAllCarWorkshopServicesByEncodedNameQueryHandler(ICarWorkshopServiceRepository carWorkshopServiceRepository,
    IUserContext userContext, IMapper mapper) : IRequestHandler<GetAllCarWorkshopServicesByEncodedNameQuery, IEnumerable<CarWorkshopServiceDto>>
{
    public async Task<IEnumerable<CarWorkshopServiceDto>> Handle(GetAllCarWorkshopServicesByEncodedNameQuery request, CancellationToken cancellationToken)
    {
        var result = await carWorkshopServiceRepository.GetAllByEncodedName(request.EncodedName);

        var dtos = mapper.Map<IEnumerable<CarWorkshopServiceDto>>(result);

        return dtos;
    }
}
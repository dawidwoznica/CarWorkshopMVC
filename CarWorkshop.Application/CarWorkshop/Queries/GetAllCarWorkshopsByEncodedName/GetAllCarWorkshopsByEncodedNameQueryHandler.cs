using AutoMapper;
using CarWorkshop.Domain.Interfaces;
using MediatR;

namespace CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshopsByEncodedName
{
    public class GetAllCarWorkshopsByEncodedNameQueryHandler(
        ICarWorkshopRepository carWorkshopRepository,
        IMapper mapper)
        : IRequestHandler<GetAllCarWorkshopsByEncodedNameQuery, CarWorkshopDto>
    {
        public async Task<CarWorkshopDto> Handle(GetAllCarWorkshopsByEncodedNameQuery request, CancellationToken cancellationToken)
        {
            var carWorkshop = await carWorkshopRepository.GetByEncodedName(request.EncodedName);

            var carWorkshopDto = mapper.Map<CarWorkshopDto>(carWorkshop);

            return carWorkshopDto;
        }
    }
}
